using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Departments.CreateDepartment;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Departments.CreateDepartment;

internal sealed class Request
{
    public string Name { get; set; } = null!;
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial CreateDepartmentCommand ToCommand(this Request request);
}
