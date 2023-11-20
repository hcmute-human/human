using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Employees.GetEmployees;

public sealed class GetEmployeesHandler : ICommandHandler<GetEmployeesCommand, Result<GetEmployeesResult>>
{
    private readonly IAppDbContext dbContext;

    public GetEmployeesHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<GetEmployeesResult>> ExecuteAsync(GetEmployeesCommand command, CancellationToken ct)
    {
        var query = dbContext.Employees.AsQueryable();
        if (!string.IsNullOrEmpty(command.FirstName))
        {
            query = query.Where(x => x.FirstName.Contains(command.FirstName));
        }
        if (!string.IsNullOrEmpty(command.LastName))
        {
            query = query.Where(x => x.LastName.Contains(command.LastName));
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);

        query = command.Order.SortOrDefault(query, x => x.OrderByDescending(x => x.CreatedTime));
        var employees = await query.Skip(command.Offset).Take(command.Size).ToArrayAsync(ct).ConfigureAwait(false) ?? Array.Empty<Employee>();
        return new GetEmployeesResult { TotalCount = totalCount, Items = employees.ToItems(), };
    }
}
