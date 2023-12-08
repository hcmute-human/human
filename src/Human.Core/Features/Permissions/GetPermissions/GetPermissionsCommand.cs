using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.Permissions.GetPermissions;

public class GetPermissionsCommand : Collective, ICommand<Result<GetPermissionsResult>>
{
    public string? Name { get; set; }
}
