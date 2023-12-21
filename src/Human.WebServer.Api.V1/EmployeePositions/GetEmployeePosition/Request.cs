using FastEndpoints;
using FluentValidation;
using Human.Core.Features.EmployeePositions.GetEmployeePosition;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.EmployeePositions.GetEmployeePosition;

internal sealed class Request : Collective
{
    public required Guid EmployeeId { get; set; }
    public required Guid DepartmentPositionId { get; set; }
    public bool? IncludeDepartment { get; set; }
    public bool? IncludeDepartmentPosition { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetEmployeePositionCommand ToCommand(this Request request);
}
