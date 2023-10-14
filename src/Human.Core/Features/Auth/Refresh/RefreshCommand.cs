using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Auth.Refresh;

public class RefreshCommand : ICommand<Result<RefreshResult>>
{
    public required Guid RefreshToken { get; init; }
}
