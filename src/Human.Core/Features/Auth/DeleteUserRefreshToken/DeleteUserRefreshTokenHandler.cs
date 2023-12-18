using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.UserRefreshTokens.DeleteUserRefreshToken;

public sealed class DeleteUserRefreshTokenHandler(IAppDbContext dbContext) : ICommandHandler<DeleteUserRefreshTokenCommand, Result>
{
    private readonly IAppDbContext dbContext = dbContext;

    public async Task<Result> ExecuteAsync(DeleteUserRefreshTokenCommand command, CancellationToken ct)
    {
        var count = await dbContext.UserRefreshTokens
            .Where(x => x.Token == command.Token)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result.Fail("Refresh token not found")
                .WithName(nameof(command.Token))
                .WithCode("not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }
        return Result.Ok();
    }
}
