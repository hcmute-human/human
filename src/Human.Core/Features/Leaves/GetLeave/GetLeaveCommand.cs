using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Leaves.GetLeave;

public class GetLeaveCommand : ICommand<Result<GetLeaveResult>>
{
    public Guid? LeaveTypeId { get; set; }
    public Guid? IssuerId { get; set; }
    public int? Year { get; set; }
}
