using FastEndpoints;
using FluentResults;
using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.EmployeePositions.CreateEmployeePosition;

public class CreateEmployeePositionCommand : ICommand<Result<EmployeePosition>>
{
    public required Guid EmployeeId { get; set; }
    public required Guid DepartmentPositionId { get; set; }
    public required Instant StartTime { get; set; }
    public required Instant EndTime { get; set; }
    public required EmploymentType EmploymentType { get; set; }
    public required decimal Salary { get; set; }
}

[Mapper]
internal static partial class CreateEmployeePositionCommandMapper
{
    public static partial EmployeePosition ToEmployeePosition(this CreateEmployeePositionCommand command);
}
