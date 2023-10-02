using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Auth.Login;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.Login;

internal sealed class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

internal sealed class Validator : Validator<LoginRequest>
{
    public Validator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial LoginCommand ToCommand(this LoginRequest request);
}