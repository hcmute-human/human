using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Holidays.DeleteHoliday;

public class DeleteHolidayCommand : ICommand<Result>
{
    public required Guid Id { get; set; }
}
