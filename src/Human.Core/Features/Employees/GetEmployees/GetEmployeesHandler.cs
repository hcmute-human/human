using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Employees.GetEmployees;

public sealed class GetEmployeesHandler(IAppDbContext dbContext) : ICommandHandler<GetEmployeesCommand, Result<GetEmployeesResult>>
{
    public async Task<Result<GetEmployeesResult>> ExecuteAsync(GetEmployeesCommand command, CancellationToken ct)
    {
        var query = dbContext.Employees.AsQueryable();
        if (command.Id is not null)
        {
            query = query.Where(x => x.Id == command.Id);
        }
        if (!string.IsNullOrEmpty(command.FirstName))
        {
            query = query.Where(x => EF.Functions.ILike(x.FirstName, '%' + command.FirstName + '%'));
        }
        if (!string.IsNullOrEmpty(command.LastName))
        {
            query = query.Where(x => EF.Functions.ILike(x.LastName, '%' + command.LastName + '%'));
        }
        if (command.DepartmentId is not null)
        {
            query = query.Where(x => x.Positions.Any(x => x.DepartmentPosition.DepartmentId == command.DepartmentId));
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        if (command.CountOnly)
        {
            return new GetEmployeesResult { TotalCount = totalCount, Items = Array.Empty<Employee>(), };
        }

        query = command.Order.SortOrDefault(query, x => x.OrderByDescending(x => x.CreatedTime));
        var employees = await query.Skip(command.Offset).Take(command.Size).ToArrayAsync(ct).ConfigureAwait(false) ?? Array.Empty<Employee>();
        return new GetEmployeesResult { TotalCount = totalCount, Items = employees };
    }
}
