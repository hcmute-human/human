
using FastEndpoints;
using FluentValidation;
using Riok.Mapperly.Abstractions;
using Human.Core.Features.Employees.GetEmployee;

namespace Human.WebServer.Api.V1.Employees.GetEmployee;

internal sealed class Request
{
    public Guid? Id { get; set; }
}

internal sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetEmployeeCommand ToCommand(this Request request);
}