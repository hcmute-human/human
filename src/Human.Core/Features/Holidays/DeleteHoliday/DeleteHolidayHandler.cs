using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Holidays.DeleteHoliday;

public sealed class DeleteHolidayHandler(IAppDbContext dbContext) : ICommandHandler<DeleteHolidayCommand, Result>
{
    public async Task<Result> ExecuteAsync(DeleteHolidayCommand command, CancellationToken ct)
    {
        var count = await dbContext.Holidays
            .Where(x => x.Id == command.Id)
            .ExecuteDeleteAsync(ct)
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
