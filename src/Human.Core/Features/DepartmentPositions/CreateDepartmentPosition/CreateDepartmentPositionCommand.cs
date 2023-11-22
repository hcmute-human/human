using FastEndpoints;
using FluentResults;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.DepartmentPositions.CreateDepartmentPosition;

public class CreateDepartmentPositionCommand : ICommand<Result<DepartmentPosition>>
{
    public required Guid DepartmentId { get; set; }
    public required string Name { get; set; }
}

[Mapper]
internal static partial class CreateDepartmentPositionCommandMapper
{
    public static partial DepartmentPosition ToDepartmentPosition(this CreateDepartmentPositionCommand command);
}
