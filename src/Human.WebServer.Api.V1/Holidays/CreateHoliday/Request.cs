using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Holidays.CreateHoliday;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Holidays.CreateHoliday;

internal sealed class Request
{
    public string Name { get; set; } = null!;
    public Instant StartTime { get; set; }
    public Instant? EndTime { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.StartTime).NotNull();
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial CreateHolidayCommand ToCommand(this Request request);
}
