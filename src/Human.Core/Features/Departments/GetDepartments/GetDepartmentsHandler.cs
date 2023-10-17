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
        var departments = await dbContext.Departments
            .OrderBy(x => x.CreatedTime)
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);
        return departments.ToResult();
    }
}
