using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.UserPermissions.DeleteUserPermission;

public class DeleteUserPermissionCommand : ICommand<Result>
{
    public required Guid UserId { get; set; }
    public required string Permission { get; set; }
}
