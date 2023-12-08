using FastEndpoints;
using FluentResults;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.UserPermissions.CreateUserPermission;

public class CreateUserPermissionCommand : ICommand<Result<UserPermission>>
{
    public required Guid UserId { get; set; }
    public required string Permission { get; set; }
}

[Mapper]
internal static partial class CreateUserPermissionCommandMapper
{
    public static partial UserPermission ToUserPermission(this CreateUserPermissionCommand command);
}
