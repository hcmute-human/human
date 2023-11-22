using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Human.Core.Features.EmployeePositions.GetEmployeePositions;

public sealed class GetEmployeePositionsHandler : ICommandHandler<GetEmployeePositionsCommand, Result<GetEmployeePositionsResult>>
{
    private readonly IAppDbContext dbContext;

    public GetEmployeePositionsHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<GetEmployeePositionsResult>> ExecuteAsync(GetEmployeePositionsCommand command, CancellationToken ct)
    {
        var query = dbContext.EmployeePositions.Where(x => x.EmployeeId == command.EmployeeId);
        if (!string.IsNullOrEmpty(command.Name))
        {
            query = query.Where(x => x.DepartmentPosition.Name.Contains(command.Name));
        }
        if (!string.IsNullOrEmpty(command.DepartmentName))
        {
            query = query.Where(x => x.DepartmentPosition.Department.Name.Contains(command.DepartmentName));
        }
        query = (command.IncludeDepartment, command.IncludeDepartmentPosition) switch
        {
            (true, true) or (true, false) => query.Include(x => x.DepartmentPosition).ThenInclude(x => x.Department),
            (false, true) => query.Include(x => x.DepartmentPosition),
            _ => query,
        };

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        query = command.Order.SortOrDefault(query, x => x.OrderByDescending(x => x.CreatedTime));

        var positions = await query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct).ConfigureAwait(false);
        return new GetEmployeePositionsResult
        {
            TotalCount = totalCount,
            Items = positions
        };
    }
}
