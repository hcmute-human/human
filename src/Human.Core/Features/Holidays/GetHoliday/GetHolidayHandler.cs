using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Holidays.GetHoliday;

public sealed class GetHolidayHandler(IAppDbContext dbContext) : ICommandHandler<GetHolidayCommand, Result<Holiday>>
{
    private readonly IAppDbContext dbContext = dbContext;

    public async Task<Result<Holiday>> ExecuteAsync(GetHolidayCommand command, CancellationToken ct)
    {
        var holiday = await dbContext.Holidays.FirstOrDefaultAsync(x => x.Id == command.Id, ct).ConfigureAwait(false);
        if (holiday is null)
        {
            return Result.Fail("Holiday does not exist")
              .WithName(nameof(command.Id))
              .WithCode("not_found")
              .WithStatus(HttpStatusCode.NotFound);
        }
        return holiday;
    }
}
