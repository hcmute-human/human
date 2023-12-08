using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.LeaveApplications.DeleteLeaveApplication;

public class DeleteLeaveApplicationCommand : ICommand<Result>
{
    public required Guid Id { get; set; }
}
