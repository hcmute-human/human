using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Auth.CreatePasswordResetToken;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.CreatePasswordResetToken;

internal sealed class CreatePasswordResetTokenRequest
{
    public required string Email { get; set; }
}

internal sealed class Validator : Validator<CreatePasswordResetTokenRequest>
{
    public Validator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial CreatePasswordResetTokenCommand ToCommand(this CreatePasswordResetTokenRequest createPasswordResetTokenRequest);
}
