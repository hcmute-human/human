using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.DepartmentPositions.DeleteDepartmentPosition;

public class DeleteDepartmentPositionCommand : ICommand<Result>
{
    public required Guid Id { get; set; }
}
