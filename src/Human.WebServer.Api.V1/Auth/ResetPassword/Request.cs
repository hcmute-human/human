using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Auth.ResetPassword;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.ResetPassword;

internal sealed class ResetPasswordRequest
{
    public required string Token { get; set; }
    public required string Password { get; set; }
}

internal sealed class Validator : Validator<ResetPasswordRequest>
{
    public Validator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(7);
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial ResetPasswordCommand ToCommand(this ResetPasswordRequest createPasswordResetTokenRequest);
}
