using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.EmployeePositions.GetEmployeePositions;

public class GetEmployeePositionsCommand : Collective, ICommand<Result<GetEmployeePositionsResult>>
{
    public required Guid EmployeeId { get; set; }
    public string? Name { get; set; }
    public string? DepartmentName { get; set; }
    public bool? IncludeDepartment { get; set; }
    public bool? IncludeDepartmentPosition { get; set; }
}
