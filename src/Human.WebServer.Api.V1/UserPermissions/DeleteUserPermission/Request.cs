using Human.Core.Features.UserPermissions.DeleteUserPermission;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.UserPermissions.DeleteUserPermission;

internal sealed class Request
{
    public required Guid UserId { get; set; }
    public required string Permission { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteUserPermissionCommand ToCommand(this Request request);
}
