using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Auth.CreatePasswordResetToken;

public class CreatePasswordResetTokenCommand : ICommand<Result<CreatePasswordResetTokenResult>>
{
    public required string Email { get; init; }
}
