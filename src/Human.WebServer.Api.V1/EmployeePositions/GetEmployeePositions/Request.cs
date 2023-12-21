using FastEndpoints;
using FluentValidation;
using Human.Core.Features.EmployeePositions.GetEmployeePositions;
using Human.Core.Models;
using Human.Domain.Constants;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.EmployeePositions.GetEmployeePositions;

internal sealed class Request : Collective
{
    public required Guid EmployeeId { get; set; }
    public string? Name { get; set; }
    public string? DepartmentName { get; set; }
    public bool? IncludeDepartment { get; set; }
    public bool? IncludeDepartmentPosition { get; set; }

    [HasPermission(Permit.ReadEmployeePosition, IsRequired = false)]
    public bool HasReadPermission { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetEmployeePositionsCommand ToCommand(this Request request);
}
