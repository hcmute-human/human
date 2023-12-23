using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Holidays.CreateHoliday;

public sealed class CreateHolidayHandler(IAppDbContext dbContext) : ICommandHandler<CreateHolidayCommand, Result<Holiday>>
{
    public async Task<Result<Holiday>> ExecuteAsync(CreateHolidayCommand command, CancellationToken ct)
    {
        if (await dbContext.Holidays.AnyAsync(x => x.StartTime <= command.EndTime && x.EndTime >= command.StartTime, ct).ConfigureAwait(false))
        {
            return Result
                .Fail("Time of holiday is overlapped")
                .WithName(nameof(command.StartTime))
                .WithCode("time_overlap")
                .WithStatus(HttpStatusCode.BadRequest)
                .WithError("Time of holiday is overlapped")
                .WithName(nameof(command.EndTime))
                .WithCode("time_overlap")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var holiday = command.ToHoliday();
        dbContext.Add(holiday);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return holiday;
    }
}
