using FastEndpoints;
using FluentResults;
using Human.Domain.Models;

namespace Human.Core.Features.Holidays.GetHoliday;

public sealed class GetHolidayCommand : ICommand<Result<Holiday>>
{
    public required Guid Id { get; set; }
}
