using FastEndpoints;
using FluentValidation;
using Human.Core.Features.DepartmentPositions.DeleteDepartmentPosition;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.DepartmentPositions.DeleteDepartmentPosition;

internal sealed class Request
{
    public Guid? Id { get; set; }
}

internal sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteDepartmentPositionCommand ToCommand(this Request request);
}
