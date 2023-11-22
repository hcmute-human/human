using FastEndpoints;
using FluentValidation;
using Human.Core.Features.EmployeePositions.UpdateEmployeePosition;
using Human.Domain.Constants;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.EmployeePositions.UpdateEmployeePosition;

internal sealed class Request
{
    public Guid? EmployeeId { get; set; }
    public Guid? DepartmentPositionId { get; set; }
    public Instant? StartTime { get; set; }
    public Instant? EndTime { get; set; }
    public EmploymentType? EmploymentType { get; set; }
    public decimal? Salary { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.EmployeeId).NotNull();
        RuleFor(x => x.DepartmentPositionId).NotNull();
        RuleFor(x => x.StartTime).NotNull();
        RuleFor(x => x.EndTime).NotNull();
        RuleFor(x => x.EmploymentType).NotNull();
        RuleFor(x => x.Salary).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial UpdateEmployeePositionCommand ToCommand(this Request request);
}
