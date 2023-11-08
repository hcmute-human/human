using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Departments.DeleteDepartment;

public sealed class DeleteDepartmentHandler : ICommandHandler<DeleteDepartmentCommand, Result>
{
    private readonly IAppDbContext dbContext;

    public DeleteDepartmentHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> ExecuteAsync(DeleteDepartmentCommand command, CancellationToken ct)
    {
        var count = await dbContext.Departments
            .Where(x => x.Id == command.Id)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result.Fail("Department not found")
                .WithName(nameof(command.Id))
                .WithCode("department_not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }
        return Result.Ok();
    }
}
