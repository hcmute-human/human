using FastEndpoints;
using FluentValidation;
using Human.Core.Features.EmployeePositions.GetEmployeePositions;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.EmployeePositions.GetEmployeePositions;

internal sealed class Request : Collective
{
    public Guid? EmployeeId { get; set; }
    public string? Name { get; set; }
    public string? DepartmentName { get; set; }
    public bool? IncludeDepartment { get; set; }
    public bool? IncludeDepartmentPosition { get; set; }
}

internal sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetEmployeePositionsCommand ToCommand(this Request request);
}
