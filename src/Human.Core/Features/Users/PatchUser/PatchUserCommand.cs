using FastEndpoints;
using FluentResults;
using Human.Domain.Models;
using SystemTextJsonPatch;

namespace Human.Core.Features.Users.PatchUser;

public sealed class PatchUserCommand : ICommand<Result<User>>
{
    public Guid Id { get; set; }
    public required JsonPatchDocument<User> Patch { get; set; }
}
