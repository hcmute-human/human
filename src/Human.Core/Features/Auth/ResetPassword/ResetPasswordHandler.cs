using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Human.Core.Features.Auth.ResetPassword;

public sealed class ResetPasswordHandler : ICommandHandler<ResetPasswordCommand, Result<ResetPasswordResult>>
{
    private readonly IAppDbContext dbContext;

    public ResetPasswordHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<ResetPasswordResult>> ExecuteAsync(ResetPasswordCommand command, CancellationToken ct)
    {
        var token = await dbContext.UserPasswordResetTokens.Where(x => x.Token == command.Token)
            .Select(x => new { UserId = x.User.Id, x.ExpirationTime })
            .FirstOrDefaultAsync(cancellationToken: ct)
            .ConfigureAwait(false);
        if (token is null)
        {
            return Result.Fail("Token does not exist")
                .WithName(nameof(command.Token))
                .WithCode("token_not_found")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        await dbContext.UserPasswordResetTokens
            .Where(x => x.Token == command.Token)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (SystemClock.Instance.GetCurrentInstant() > token.ExpirationTime)
        {
            return Result.Fail("Token has already expired")
                .WithName(nameof(command.Token))
                .WithCode("token_expired")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(command.Password);
        await dbContext.Users
            .Where(x => x.Id == token.UserId)
            .ExecuteUpdateAsync(u => u.SetProperty(x => x.PasswordHash, hash), ct)
            .ConfigureAwait(false);
        return new ResetPasswordResult();
    }
}
