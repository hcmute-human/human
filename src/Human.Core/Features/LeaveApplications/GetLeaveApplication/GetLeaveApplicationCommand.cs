using FastEndpoints;
using FluentResults;
using Human.Domain.Models;

namespace Human.Core.Features.LeaveApplications.GetLeaveApplication;

public sealed class GetLeaveApplicationCommand : ICommand<Result<LeaveApplication>>
{
    public required Guid Id { get; set; }
    public bool? IncludeIssuer { get; set; }
    public bool? IncludeLeaveType { get; set; }
    public bool? IncludeProcessor { get; set; }
}
