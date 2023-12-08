using FastEndpoints;
using FluentResults;
using Human.Domain.Models;
using SystemTextJsonPatch;

namespace Human.Core.Features.LeaveApplications.PatchLeaveApplication;

public sealed class PatchLeaveApplicationCommand : ICommand<Result<LeaveApplication>>
{
    public Guid Id { get; set; }
    public required JsonPatchDocument<LeaveApplication> Patch { get; set; }
}
