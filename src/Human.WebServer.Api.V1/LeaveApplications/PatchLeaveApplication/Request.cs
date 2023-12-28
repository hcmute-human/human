using System.Security.Claims;
using FastEndpoints;
using FluentValidation;
using Human.Core.Features.LeaveApplications.PatchLeaveApplication;
using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;
using SystemTextJsonPatch;

namespace Human.WebServer.Api.V1.LeaveApplications.PatchLeaveApplication;

internal sealed class Request
{
    public Guid Id { get; set; }
    public JsonPatchDocument<Payload>? Patch { get; set; }
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid ProcessorId { get; set; }

    [HasPermission(Permit.UpdateLeaveApplication, IsRequired = false)]
    public bool CanUpdate { get; set; }
    [HasPermission(Permit.ProcessLeaveApplication, IsRequired = false)]
    public bool CanProcess { get; set; }

    public sealed class Payload
    {
        public Guid IssuerId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public Instant StartTime { get; set; }
        public Instant EndTime { get; set; }
        public LeaveApplicationStatus Status { get; set; }
        public string? Description { get; set; }
        public Guid? ProcessorId { get; set; }
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
    public static partial PatchLeaveApplicationCommand ToCommand(this Request request);
}
