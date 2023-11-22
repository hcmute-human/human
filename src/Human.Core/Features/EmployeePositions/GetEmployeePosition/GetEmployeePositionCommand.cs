using FastEndpoints;
using FluentResults;
using Human.Core.Models;
using Human.Domain.Models;

namespace Human.Core.Features.EmployeePositions.GetEmployeePosition;

public class GetEmployeePositionCommand : Collective, ICommand<Result<EmployeePosition>>
{
    public required Guid EmployeeId { get; set; }
    public required Guid DepartmentPositionId { get; set; }
    public bool? IncludeDepartment { get; set; }
    public bool? IncludeDepartmentPosition { get; set; }
}
