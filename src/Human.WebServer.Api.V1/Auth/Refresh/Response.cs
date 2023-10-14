using Human.Core.Features.Auth.Refresh;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.Refresh;

internal sealed class Response
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this RefreshResult result);
}
