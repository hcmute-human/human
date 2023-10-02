using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Auth.ResetPassword;

public class ResetPasswordCommand : ICommand<Result<ResetPasswordResult>>
{
    public required string Token { get; set; }
    public required string Password { get; set; }
}
