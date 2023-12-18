using Human.Core.Features.UserRefreshTokens.DeleteUserRefreshToken;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.UserRefreshTokens.DeleteUserRefreshToken;

internal sealed class Request
{
    public required Guid Token { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteUserRefreshTokenCommand ToCommand(this Request request);
}
