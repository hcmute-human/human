using System.Security.Claims;
using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Auth.Authorize;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.Authorize;

internal sealed class Request
{
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid UserId { get; set; }
    public ICollection<string> Permissions { get; set; } = Array.Empty<string>();
    public bool AllPermissions { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Permissions)
            .NotEmpty();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial AuthorizeCommand ToCommand(this Request request);
}
