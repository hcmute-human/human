using FastEndpoints;
using FluentResults;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Users.CreateUser;

public sealed class CreateUserCommand : ICommand<Result<User>>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

[Mapper]
internal static partial class CreateUserCommandMapper
{
    [MapProperty(nameof(command.Password), nameof(User.PasswordHash))]
    public static partial User ToUser(this CreateUserCommand command);
}
