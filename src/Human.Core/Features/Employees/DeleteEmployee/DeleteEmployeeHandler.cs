using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Employees.DeleteEmployee;

public sealed class DeleteEmployeeHandler(IAppDbContext dbContext) : ICommandHandler<DeleteEmployeeCommand, Result>
{
    public async Task<Result> ExecuteAsync(DeleteEmployeeCommand command, CancellationToken ct)
    {
        var count = await dbContext.Users
            .Where(x => x.Id == command.Id)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result.Fail("Employee not found")
                .WithName(nameof(command.Id))
                .WithCode("employee_not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }
        return Result.Ok();
    }
}
