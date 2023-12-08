using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;

namespace Human.Core.Features.UserPermissions.CreateUserPermission;

public sealed class CreateUserPermissionHandler : ICommandHandler<CreateUserPermissionCommand, Result<UserPermission>>
{
    private readonly IAppDbContext dbContext;

    public CreateUserPermissionHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<UserPermission>> ExecuteAsync(CreateUserPermissionCommand command, CancellationToken ct)
    {
        var userPermission = command.ToUserPermission();
        dbContext.Add(userPermission);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return userPermission;
    }
}
