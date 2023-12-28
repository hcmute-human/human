using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Users.PatchUser;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;
using SystemTextJsonPatch;

namespace Human.WebServer.Api.V1.Users.PatchUser;

internal sealed class Request
{
    public Guid Id { get; set; }
    public JsonPatchDocument<Payload>? Patch { get; set; }

    public sealed class Payload
    {
        public AssetInfo? Avatar { get; set; }
    }
}

internal sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Patch).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial PatchUserCommand ToCommand(this Request request);
}
