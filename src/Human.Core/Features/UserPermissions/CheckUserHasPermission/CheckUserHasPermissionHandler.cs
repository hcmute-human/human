using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.UserPermissions.CheckUserHasPermission;

public sealed class DeleteUserPermissionHandler(IAppDbContext dbContext) : ICommandHandler<DeleteUserPermissionCommand, Result>
{
    private readonly IAppDbContext dbContext = dbContext;

    public async Task<Result> ExecuteAsync(DeleteUserPermissionCommand command, CancellationToken ct)
    {
        var any = await dbContext.UserPermissions
            .AnyAsync(x => x.UserId == command.UserId && x.Permission == command.Permission, ct)
            .ConfigureAwait(false);
        if (!any)
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
