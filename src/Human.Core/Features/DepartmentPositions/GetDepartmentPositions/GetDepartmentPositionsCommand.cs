using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.DepartmentPositions.GetDepartmentPositions;

public class GetDepartmentPositionsCommand : Collective, ICommand<Result<GetDepartmentPositionsResult>>
{
    public Guid? DepartmentId { get; set; }
    public string? Name { get; set; }
}
