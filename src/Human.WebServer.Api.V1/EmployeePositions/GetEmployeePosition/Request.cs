using FastEndpoints;
using FluentValidation;
using Human.Core.Features.EmployeePositions.GetEmployeePosition;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.EmployeePositions.GetEmployeePosition;

internal sealed class Request : Collective
{
    public Guid? EmployeeId { get; set; }
    public Guid? DepartmentPositionId { get; set; }
    public bool? IncludeDepartment { get; set; }
    public bool? IncludeDepartmentPosition { get; set; }
}

internal sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull();
        RuleFor(x => x.DepartmentPositionId).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetEmployeePositionCommand ToCommand(this Request request);
}
