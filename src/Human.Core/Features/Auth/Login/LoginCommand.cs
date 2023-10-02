using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Auth.Login;

public class LoginCommand : ICommand<Result<LoginResult>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
