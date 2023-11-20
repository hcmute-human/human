using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Employees.GetEmployee;

public sealed class GetEmployeeHandler : ICommandHandler<GetEmployeeCommand, Result<GetEmployeeResult>>
{
    private readonly IAppDbContext dbContext;

    public GetEmployeeHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<GetEmployeeResult>> ExecuteAsync(GetEmployeeCommand command, CancellationToken ct)
    {
        var result = await dbContext.Employees.Where(x => x.User.Id == command.Id).Select(x => new { User = new User { Id = x.User.Id }, Employee = x }).FirstOrDefaultAsync(ct).ConfigureAwait(false);
        if (result is null)
        {
            return Result.Fail("Employee does not exist")
              .WithName(nameof(command.Id))
              .WithCode("invalid_id")
              .WithStatus(HttpStatusCode.NotFound);
        }
        result.Employee.User = result.User;
        return result.Employee.ToResult();
    }
}
