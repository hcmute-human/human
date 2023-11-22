using FastEndpoints;
using FluentValidation;
using Human.Core.Features.DepartmentPositions.GetDepartmentPositions;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.DepartmentPositions.GetDepartmentPositions;

internal sealed class Request : Collective
{
    public Guid? DepartmentId { get; set; }
    public string? Name { get; set; }
}

internal sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.DepartmentId).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetDepartmentPositionsCommand ToCommand(this Request request);
}
