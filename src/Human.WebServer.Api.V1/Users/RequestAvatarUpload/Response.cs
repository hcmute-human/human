using Human.Core.Features.Users.RequestAvatarUpload;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Users.RequestAvatarUpload;

internal sealed class Response
{
    public required string UploadUrl { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this RequestAvatarUploadResult result);
}
