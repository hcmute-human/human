using System.Security.Claims;
using FastEndpoints;
using Human.Core.Features.UserPermissions.GetUserPermissions;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.UserPermissions.GetUserPermissions;

internal sealed class Request : Collective
{
    public required Guid UserId { get; set; }
    public string? Name { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetUserPermissionsCommand ToCommand(this Request request);
}
