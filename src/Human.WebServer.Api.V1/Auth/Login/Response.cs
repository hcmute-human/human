using Human.Core.Features.Auth.Login;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.Login;

internal sealed class LoginResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial LoginResponse ToResponse(this LoginResult result);
}