using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Employees.CreateEmployee;

public sealed class CreateEmployeeHandler : ICommandHandler<CreateEmployeeCommand, Result<Employee>>
{
    private readonly IAppDbContext dbContext;

    public CreateEmployeeHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<Employee>> ExecuteAsync(CreateEmployeeCommand command, CancellationToken ct)
    {
        var anyEmployee = await dbContext.Employees.AnyAsync(x => x.User.Id == command.User.Id, ct).ConfigureAwait(false);
        if (anyEmployee)
        {
            return Result.Fail("Employee already exists")
              .WithName(nameof(command.User))
              .WithCode("duplicated_id")
              .WithStatus(HttpStatusCode.BadRequest);
        }

        var employee = command.ToEmployee();
        dbContext.Add(employee);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return employee;
    }
}
