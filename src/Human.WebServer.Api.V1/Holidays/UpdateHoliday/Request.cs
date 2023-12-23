using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Holidays.UpdateHoliday;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Holidays.UpdateHoliday;

internal sealed class Request
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Instant StartTime { get; set; }
    public Instant? EndTime { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.StartTime).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial UpdateHolidayCommand ToCommand(this Request request);
}
