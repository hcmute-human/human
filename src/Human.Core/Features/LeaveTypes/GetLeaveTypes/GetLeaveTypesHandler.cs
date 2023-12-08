using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.LeaveTypes.GetLeaveTypes;

public sealed class GetLeaveTypesHandler : ICommandHandler<GetLeaveTypesCommand, Result<GetLeaveTypesResult>>
{
    private readonly IAppDbContext dbContext;

    public GetLeaveTypesHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<GetLeaveTypesResult>> ExecuteAsync(GetLeaveTypesCommand command, CancellationToken ct)
    {
        var query = dbContext.LeaveTypes.AsQueryable();
        if (!string.IsNullOrEmpty(command.Name))
        {
            query = query.Where(x => x.Name.Contains(command.Name));
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        query = command.Order.SortOrDefault(query, x => x.OrderBy(x => x.CreatedTime));

        var leaveTypes = await query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct).ConfigureAwait(false);
        return new GetLeaveTypesResult
        {
            TotalCount = totalCount,
            Items = leaveTypes
        };
    }
}
