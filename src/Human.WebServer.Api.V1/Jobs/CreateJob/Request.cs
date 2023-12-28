using System.Security.Claims;
using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Jobs.CreateJob;
using Human.Domain.Constants;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Jobs.CreateJob;

internal sealed class Request
{
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid CreatorId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public JobStatus? Status { get; set; }
    public Guid? PositionId { get; set; }
}

internal sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Status).NotNull().IsInEnum();
        RuleFor(x => x.PositionId).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial CreateJobCommand ToCommand(this Request request);
}
