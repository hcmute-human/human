using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Departments.GetDepartments;

public sealed class GetDepartmentsHandler : ICommandHandler<GetDepartmentsCommand, Result<GetDepartmentsResult[]>>
{
    private readonly IAppDbContext dbContext;

    public GetDepartmentsHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<GetDepartmentsResult[]>> ExecuteAsync(GetDepartmentsCommand command, CancellationToken ct)
    {
        var query = dbContext.Departments.AsQueryable();
        if (!string.IsNullOrEmpty(command.Name))
        {
            query = query.Where(x => x.Name.Contains(command.Name));
        }

        query = query
            .OrderBy(x => x.CreatedTime)
            .Skip(command.Offset)
            .Take(command.Size);

        var departments = await query.ToArrayAsync(ct).ConfigureAwait(false);
        return departments.ToResult();
    }
}
