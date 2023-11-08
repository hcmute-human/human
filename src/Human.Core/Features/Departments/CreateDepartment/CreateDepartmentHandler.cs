using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;

namespace Human.Core.Features.Departments.CreateDepartment;

public sealed class CreateDepartmentHandler : ICommandHandler<CreateDepartmentCommand, Result<CreateDepartmentResult>>
{
    private readonly IAppDbContext dbContext;

    public CreateDepartmentHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<CreateDepartmentResult>> ExecuteAsync(CreateDepartmentCommand command, CancellationToken ct)
    {
        var department = new Department { Name = command.Name };
        dbContext.Add(department);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return department.ToResult();
    }
}
