using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Departments.GetDepartments;

public sealed class GetDepartmentsHandler : ICommandHandler<GetDepartmentsCommand, Result<GetDepartmentsResult>>
{
    private readonly IAppDbContext dbContext;

    public GetDepartmentsHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<GetDepartmentsResult>> ExecuteAsync(GetDepartmentsCommand command, CancellationToken ct)
    {
        var query = dbContext.Departments.AsQueryable();
        if (!string.IsNullOrEmpty(command.Name))
        {
            query = query.Where(x => x.Name.Contains(command.Name));
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        query = command.Order
            .Where(x => x.Name.EqualsEither(new[] { "name", "createdTime" }, StringComparison.OrdinalIgnoreCase))
            .SortOrDefault(query, x => x.OrderBy(x => x.CreatedTime));

        var departments = await query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct).ConfigureAwait(false);
        return new GetDepartmentsResult
        {
            TotalCount = totalCount,
            Items = departments.ToItems()
        };
    }
}
