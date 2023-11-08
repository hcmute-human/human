using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Departments.GetDepartment;

public sealed class GetDepartmentHandler : ICommandHandler<GetDepartmentCommand, Result<GetDepartmentResult>>
{
    private readonly IAppDbContext dbContext;

    public GetDepartmentHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<GetDepartmentResult>> ExecuteAsync(GetDepartmentCommand command, CancellationToken ct)
    {
        var department = await dbContext.Departments
            .Where(x => x.Id == command.Id)
            .FirstOrDefaultAsync(ct)
            .ConfigureAwait(false);

        if (department is null)
        {
            return Result.Fail("Department not found")
                .WithName(nameof(command.Id))
                .WithCode("department_not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }

        return department.ToResult();
    }
}
