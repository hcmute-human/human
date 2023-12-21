using System.Security.Claims;
using FastEndpoints;
using Human.Core.Features.Users.RequestAvatarUpload;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Users.RequestAvatarUpload;

internal sealed class Request
{
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial RequestAvatarUploadCommand ToCommand(this Request request);
}
