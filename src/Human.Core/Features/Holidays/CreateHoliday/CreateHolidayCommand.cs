using FastEndpoints;
using FluentResults;
using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Holidays.CreateHoliday;

public class CreateHolidayCommand : ICommand<Result<Holiday>>
{
    public required string Name { get; set; }
    public required Instant StartTime { get; set; }
    public required Instant EndTime { get; set; }
}

[Mapper]
internal static partial class CreateHolidayCommandMapper
{
    public static partial Holiday ToHoliday(this CreateHolidayCommand command);
}
