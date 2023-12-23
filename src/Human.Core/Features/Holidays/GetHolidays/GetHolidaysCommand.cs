using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.Holidays.GetHolidays;

public class GetHolidaysCommand : Collective, ICommand<Result<GetHolidaysResult>>
{
    public string? Name { get; set; }
    public bool? CountOnly { get; set; }
}
