using System.Security.Claims;
using FastEndpoints;
using Human.Core.Features.UserPermissions.CheckUserHasPermission;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.UserPermissions.CheckCurrentUserHasPermission;

internal sealed class Request
{
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid UserId { get; set; }
    public required string Permission { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteUserPermissionCommand ToCommand(this Request request);
}
