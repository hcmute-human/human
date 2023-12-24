using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.DepartmentPositions.GetDepartmentPositions;

public sealed class GetDepartmentPositionsHandler : ICommandHandler<GetDepartmentPositionsCommand, Result<GetDepartmentPositionsResult>>
{
    private readonly IAppDbContext dbContext;

    public GetDepartmentPositionsHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<GetDepartmentPositionsResult>> ExecuteAsync(GetDepartmentPositionsCommand command, CancellationToken ct)
    {
        var query = dbContext.DepartmentPositions.AsQueryable();
        if (command.DepartmentId is not null)
        {
            query = query.Where(x => x.DepartmentId == command.DepartmentId);
        }
        if (!string.IsNullOrEmpty(command.Name))
        {
            query = query.Where(x => EF.Functions.ILike(x.Name, '%' + command.Name + '%'));
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        query = command.Order.SortOrDefault(query, x => x.OrderByDescending(x => x.CreatedTime));

        var positions = await query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct).ConfigureAwait(false);
        return new GetDepartmentPositionsResult
        {
            TotalCount = totalCount,
            Items = positions
        };
    }
}
