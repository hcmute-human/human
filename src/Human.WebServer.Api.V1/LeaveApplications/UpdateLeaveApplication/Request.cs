using System.Security.Claims;
using FastEndpoints;
using FluentValidation;
using Human.Core.Features.LeaveApplications.UpdateLeaveApplication;
using Human.Domain.Constants;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.LeaveApplications.UpdateLeaveApplication;

internal sealed class Request
{
    public Guid Id { get; set; }
    public Guid IssuerId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public Instant StartTime { get; set; }
    public Instant EndTime { get; set; }
    public LeaveApplicationStatus Status { get; set; }
    public string? Description { get; set; }
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid? ProcessorId { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.IssuerId).NotNull();
        RuleFor(x => x.LeaveTypeId).NotNull();
        RuleFor(x => x.StartTime).NotNull();
        RuleFor(x => x.EndTime).NotNull();
        RuleFor(x => x.Status)
            .NotNull()
            .IsInEnum()
            .Must(x => x != LeaveApplicationStatus.None)
            .WithErrorCode("invalid_status")
            .WithMessage("Invalid status");
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial UpdateLeaveApplicationCommand ToCommand(this Request request);
}
