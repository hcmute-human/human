using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Human.Core.Features.Auth.CreatePasswordResetToken;

public sealed class CreatePasswordResetTokenHandler : ICommandHandler<CreatePasswordResetTokenCommand, Result<CreatePasswordResetTokenResult>>
{
    private readonly IAppDbContext dbContext;

    public CreatePasswordResetTokenHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<CreatePasswordResetTokenResult>> ExecuteAsync(CreatePasswordResetTokenCommand command, CancellationToken ct)
    {
        var user = await dbContext.Users.Where(x => x.Email == command.Email)
            .Select(x => new { x.Id })
            .FirstOrDefaultAsync(cancellationToken: ct)
            .ConfigureAwait(false);
        if (user is null)
        {
            return Result.Fail("Email does not exist")
                .WithName(nameof(command.Email))
                .WithCode("email_not_found")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var token = await dbContext.UserPasswordResetTokens.Where(x => x.User.Id == user.Id)
            .Select(x => new UserPasswordResetToken { Token = x.Token, ExpirationTime = x.ExpirationTime })
            .FirstOrDefaultAsync(ct)
            .ConfigureAwait(false);
        if (token is null)
        {
            token = new UserPasswordResetToken { User = new User { Id = user.Id }, ExpirationTime = SystemClock.Instance.GetCurrentInstant() + Duration.FromMinutes(2) };
            dbContext.Add(token);
        }
        else
        {
            if (token.ExpirationTime > SystemClock.Instance.GetCurrentInstant())
            {
                return Result.Fail("A token has already been generated before and is still active")
                    .WithCode("token_already_generated")
                    .WithStatus(HttpStatusCode.Forbidden);
            }
            token.User = new User { Id = user.Id };
            dbContext.Update(token);
        }

        dbContext.Attach(token.User);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);

        return new CreatePasswordResetTokenResult
        {
            Token = token.Token
        };
    }
}
