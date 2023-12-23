using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Holidays.GetHolidays;

public sealed class GetHolidaysHandler(IAppDbContext dbContext) : ICommandHandler<GetHolidaysCommand, Result<GetHolidaysResult>>
{
    public async Task<Result<GetHolidaysResult>> ExecuteAsync(GetHolidaysCommand command, CancellationToken ct)
    {
        var query = dbContext.Holidays.AsQueryable();
        if (command.Name is not null)
        {
            query = query.Where(x => EF.Functions.ILike(x.Name, '%' + command.Name + '%'));
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        if (command.CountOnly == true)
        {
            return new GetHolidaysResult
            {
                TotalCount = totalCount,
                Items = Array.Empty<Holiday>(),
            };
        }

        query = command.Order.SortOrDefault(query, x => x.OrderBy(x => x.CreatedTime));
        var holidays = await query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct).ConfigureAwait(false);
        return new GetHolidaysResult
        {
            TotalCount = totalCount,
            Items = holidays,
        };
    }
}
