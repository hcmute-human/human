using Human.Core.Features.Auth.CreatePasswordResetToken;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.CreatePasswordResetToken;

internal sealed class Response
{
    public required string Token { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this CreatePasswordResetTokenResult result);
}
