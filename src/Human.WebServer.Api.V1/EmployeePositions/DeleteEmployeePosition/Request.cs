using FastEndpoints;
using FluentValidation;
using Human.Core.Features.EmployeePositions.DeleteEmployeePosition;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.EmployeePositions.DeleteEmployeePosition;

internal sealed class Request
{
    public Guid? EmployeeId { get; set; }
    public Guid? DepartmentPositionId { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.EmployeeId).NotNull();
        RuleFor(x => x.DepartmentPositionId).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteEmployeePositionCommand ToCommand(this Request request);
}
