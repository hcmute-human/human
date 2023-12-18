using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.UserRefreshTokens.DeleteUserRefreshToken;

public class DeleteUserRefreshTokenCommand : ICommand<Result>
{
    public required Guid Token { get; set; }
}
