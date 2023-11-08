using System.Net;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using SystemTextJsonPatch.Exceptions;

namespace Human.Core.Features.Departments.PatchDepartment;

public sealed class PatchDepartmentHandler : ICommandHandler<PatchDepartmentCommand, Result<Department>>
{
    private readonly IAppDbContext dbContext;
    private readonly IValidator<Department> validator;

    public PatchDepartmentHandler(IAppDbContext dbContext, IValidator<Department> validator)
    {
        this.dbContext = dbContext;
        this.validator = validator;
    }

    public async Task<Result<Department>> ExecuteAsync(PatchDepartmentCommand command, CancellationToken ct)
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

        var departmentPatch = new DepartmentPatch { Name = string.Empty };
        try
        {
            command.Patch.ApplyTo(departmentPatch);
        }
        catch (JsonPatchException e)
        {
            return Result.Fail(e.Message)
                .WithName(nameof(command.Patch))
                .WithCode("patch_failed")
                .WithStatus(HttpStatusCode.BadRequest);
        }


        departmentPatch.ApplyTo(department);
        var validation = validator.Validate(department);
        if (!validation.IsValid)
        {
            return Result.Fail(validation.Errors.Select(x => new Error(x.ErrorMessage)
                .WithName(x.PropertyName)
                .WithCode(x.ErrorCode)
                .WithStatus(HttpStatusCode.BadRequest)));
        }

        dbContext.Update(department);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);

        return department;
    }
}
