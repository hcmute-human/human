using FastEndpoints;
using FluentValidation;
using Human.Core.Features.DepartmentPositions.CreateDepartmentPosition;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.DepartmentPositions.CreateDepartmentPosition;

internal sealed class Request
{
    public Guid DepartmentId { get; set; }
    public string? Name { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial CreateDepartmentPositionCommand ToCommand(this Request request);
}
