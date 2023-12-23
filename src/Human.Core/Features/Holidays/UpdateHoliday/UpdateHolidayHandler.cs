using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Human.Core.Features.Holidays.UpdateHoliday;

public sealed class UpdateHolidayHandler(IAppDbContext dbContext) : ICommandHandler<UpdateHolidayCommand, Result<Holiday>>
{
    public async Task<Result<Holiday>> ExecuteAsync(UpdateHolidayCommand command, CancellationToken ct)
    {
        if (await dbContext.Holidays.AnyAsync(x => x.Id != command.Id && x.StartTime <= command.EndTime && x.EndTime >= command.StartTime, ct).ConfigureAwait(false))
        {
            return Result
                .Fail("Time of leave already exists")
                .WithName(nameof(command.StartTime))
                .WithCode("duplicated_time")
                .WithStatus(HttpStatusCode.BadRequest)
                .WithError("Time of leave already exists")
                .WithName(nameof(command.EndTime))
                .WithCode("duplicated_time")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var count = await dbContext.Holidays
            .Where(x => x.Id == command.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.UpdatedTime, SystemClock.Instance.GetCurrentInstant())
                .SetProperty(x => x.Name, command.Name)
                .SetProperty(x => x.StartTime, command.StartTime)
                .SetProperty(x => x.EndTime, command.EndTime), ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result.Fail("Holiday not found")
                .WithName(nameof(command.Id))
                .WithCode("not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }
        return Result.Ok();
    }
}
