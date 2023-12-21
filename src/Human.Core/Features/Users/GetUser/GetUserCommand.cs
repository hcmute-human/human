using FastEndpoints;
using FluentResults;
using Human.Domain.Models;

namespace Human.Core.Features.Users.GetUser;

public sealed class GetUserCommand : ICommand<Result<User>>
{
    public required Guid Id { get; set; }
}
