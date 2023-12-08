using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.LeaveTypes.DeleteLeaveType;

public class DeleteLeaveTypeCommand : ICommand<Result>
{
    public required Guid Id { get; set; }
}
