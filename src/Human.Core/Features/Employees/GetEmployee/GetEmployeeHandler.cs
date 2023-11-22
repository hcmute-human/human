using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Employees.GetEmployee;

public sealed class GetEmployeeHandler : ICommandHandler<GetEmployeeCommand, Result<Employee>>
{
    private readonly IAppDbContext dbContext;

    public GetEmployeeHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<Employee>> ExecuteAsync(GetEmployeeCommand command, CancellationToken ct)
    {
        var employee = await dbContext.Employees.Where(x => x.Id == command.Id).FirstOrDefaultAsync(ct).ConfigureAwait(false);
        if (employee is null)
        {
            return Result.Fail("Employee does not exist")
              .WithName(nameof(command.Id))
              .WithCode("invalid_id")
              .WithStatus(HttpStatusCode.NotFound);
        }
        return employee;
    }
}
