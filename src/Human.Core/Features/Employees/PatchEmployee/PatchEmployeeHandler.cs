using System.Net;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using SystemTextJsonPatch.Exceptions;

namespace Human.Core.Features.Employees.PatchEmployee;

public sealed class PatchEmployeeHandler : ICommandHandler<PatchEmployeeCommand, Result<Employee>>
{
    private readonly IAppDbContext dbContext;
    private readonly IValidator<Employee> validator;

    public PatchEmployeeHandler(IAppDbContext dbContext, IValidator<Employee> validator)
    {
        this.dbContext = dbContext;
        this.validator = validator;
    }

    public async Task<Result<Employee>> ExecuteAsync(PatchEmployeeCommand command, CancellationToken ct)
    {
        var employee = await dbContext.Employees
            .Where(x => x.Id == command.Id)
            .FirstOrDefaultAsync(ct)
            .ConfigureAwait(false);

        if (employee is null)
        {
            return Result.Fail("Employee not found")
                .WithName(nameof(command.Id))
                .WithCode("employee_not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }

        var employeePatch = new EmployeePatch { FirstName = employee.FirstName, LastName = employee.LastName, DateOfBirth = employee.DateOfBirth };
        try
        {
            command.Patch.ApplyTo(employeePatch);
        }
        catch (JsonPatchException e)
        {
            return Result.Fail(e.Message)
                .WithName(nameof(command.Patch))
                .WithCode("patch_failed")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        employeePatch.ApplyTo(employee);
        var validation = validator.Validate(employee);
        if (!validation.IsValid)
        {
            return Result.Fail(validation.Errors.Select(x => new Error(x.ErrorMessage)
                .WithName(x.PropertyName)
                .WithCode(x.ErrorCode)
                .WithStatus(HttpStatusCode.BadRequest)));
        }

        dbContext.Update(employee);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return employee;
    }
}
