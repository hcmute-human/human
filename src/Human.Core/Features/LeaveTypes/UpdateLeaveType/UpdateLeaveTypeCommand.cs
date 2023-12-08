using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.LeaveTypes.UpdateLeaveType;

public class UpdateLeaveTypeCommand : ICommand<Result>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int Days { get; set; }
}
