using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Tests.GetTests;

public sealed class GetTestsHandler(IAppDbContext dbContext) : ICommandHandler<GetTestsCommand, Result<GetTestsResult>>
{
    public async Task<Result<GetTestsResult>> ExecuteAsync(GetTestsCommand command, CancellationToken ct)
    {
        var query = dbContext.Tests.AsQueryable();
        if (command.Name is not null)
        {
            query = query.Where(x => EF.Functions.ILike(x.Name, '%' + command.Name + '%'));
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        if (command.CountOnly == true)
        {
            return new GetTestsResult
            {
                TotalCount = totalCount,
                Items = Array.Empty<Test>(),
            };
        }

        query = command.Order.SortOrDefault(query, x => x.OrderBy(x => x.CreatedTime));
        var tests = await query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct).ConfigureAwait(false);
        return new GetTestsResult
        {
            TotalCount = totalCount,
            Items = tests,
        };
    }
}
