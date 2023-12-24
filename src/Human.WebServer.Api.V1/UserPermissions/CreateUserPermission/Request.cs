using FastEndpoints;
using FluentValidation;
using Human.Core.Features.UserPermissions.CreateUserPermission;
using Human.Domain.Constants;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.UserPermissions.CreateUserPermission;

internal sealed class Request
{
    public Guid UserId { get; set; }
    public string? Permission { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Permission)
            .NotEmpty()
            .Must(x => Permit.AllPermissions.Contains(x!, StringComparer.Ordinal));
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial CreateUserPermissionCommand ToCommand(this Request request);
}
