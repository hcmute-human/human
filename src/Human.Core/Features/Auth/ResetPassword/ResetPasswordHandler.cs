using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        var user = await dbContext.UserPasswordResetTokens.Where(x => x.Token == command.Token)
            .Select(x => new { x.User.Id })
            .FirstOrDefaultAsync(cancellationToken: ct)
            .ConfigureAwait(false);
        if (user is null)
        {
            return Result.Fail("Token does not exist")
                .WithName(nameof(command.Token))
                .WithCode("token_not_found")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(command.Password);
        await dbContext.UserPasswordResetTokens
            .Where(x => x.Token == command.Token)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        await dbContext.Users
            .Where(x => x.Id == user.Id)
            .ExecuteUpdateAsync(u => u.SetProperty(x => x.PasswordHash, hash), ct)
            .ConfigureAwait(false);
        return new ResetPasswordResult();
    }
}
