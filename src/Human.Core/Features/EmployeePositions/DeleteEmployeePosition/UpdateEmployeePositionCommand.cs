using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.EmployeePositions.DeleteEmployeePosition;

public class DeleteEmployeePositionCommand : ICommand<Result>
{
    public required Guid EmployeeId { get; set; }
    public required Guid DepartmentPositionId { get; set; }
}
