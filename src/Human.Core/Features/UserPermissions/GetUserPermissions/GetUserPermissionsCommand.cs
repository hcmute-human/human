using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.UserPermissions.GetUserPermissions;

public class GetUserPermissionsCommand : Collective, ICommand<Result<GetUserPermissionsResult>>
{
    public required Guid UserId { get; set; }
    public string? Permission { get; set; }
}
