using FastEndpoints;
using FluentResults;
using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Holidays.UpdateHoliday;

public class UpdateHolidayCommand : ICommand<Result<Holiday>>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Instant StartTime { get; set; }
    public required Instant EndTime { get; set; }
}

[Mapper]
internal static partial class UpdateHolidayCommandMapper
{
    public static partial Holiday ToHoliday(this UpdateHolidayCommand command);
}
