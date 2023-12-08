using Human.Core.Features.Permissions.GetPermissions;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Permissions.GetPermissions;

internal sealed class Request : Collective
{
    public string? Name { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetPermissionsCommand ToCommand(this Request request);
}
