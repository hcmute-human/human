using Human.Core.Features.Auth.CreatePasswordResetToken;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.CreatePasswordResetToken;

internal sealed class CreatePasswordResetTokenResponse
{
    public required string Token { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial CreatePasswordResetTokenResponse ToResponse(this CreatePasswordResetTokenResult result);
}
