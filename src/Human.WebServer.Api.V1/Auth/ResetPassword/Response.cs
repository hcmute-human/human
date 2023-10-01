using Human.Core.Features.Auth.ResetPassword;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.ResetPassword;

internal sealed class ResetPasswordResponse
{
    public required string Token { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial ResetPasswordResponse ToResponse(this ResetPasswordResult result);
}
