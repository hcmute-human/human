using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Auth.Authorize;

public class AuthorizeCommand : ICommand<Result<bool>>
{
    public required Guid UserId { get; set; }
    public required ICollection<string> Permissions { get; set; }
    public required bool AllPermissions { get; set; }
}
