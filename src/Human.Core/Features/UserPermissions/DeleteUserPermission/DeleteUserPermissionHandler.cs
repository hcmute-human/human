using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.UserPermissions.DeleteUserPermission;

public sealed class DeleteUserPermissionHandler(IAppDbContext dbContext) : ICommandHandler<DeleteUserPermissionCommand, Result>
{
    private readonly IAppDbContext dbContext = dbContext;

    public async Task<Result> ExecuteAsync(DeleteUserPermissionCommand command, CancellationToken ct)
    {
        var count = await dbContext.UserPermissions
            .Where(x => x.UserId == command.UserId && x.Permission == command.Permission)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result
                .Fail("Permission not found")
                .WithName(nameof(command.UserId))
                .WithCode("permission_not_found")
                .WithStatus(HttpStatusCode.NotFound)
                .WithError("Permission not found")
                .WithName(nameof(command.Permission))
                .WithCode("permission_not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }
        return Result.Ok();
    }
}
