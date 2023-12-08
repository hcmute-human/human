using FastEndpoints;
using FluentValidation;
using Human.Core.Features.LeaveTypes.CreateLeaveType;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.LeaveTypes.CreateLeaveType;

internal sealed class Request
{
    public string? Name { get; set; }
    public int? Days { get; set; }
    public string? Description { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Days).NotNull().GreaterThan(0);
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial CreateLeaveTypeCommand ToCommand(this Request request);
}
