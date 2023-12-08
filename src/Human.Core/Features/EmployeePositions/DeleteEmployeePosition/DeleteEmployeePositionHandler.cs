using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.EmployeePositions.DeleteEmployeePosition;

public sealed class DeleteEmployeePositionHandler : ICommandHandler<DeleteEmployeePositionCommand, Result>
{
    private readonly IAppDbContext dbContext;

    public DeleteEmployeePositionHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> ExecuteAsync(DeleteEmployeePositionCommand command, CancellationToken ct)
    {
        var count = await dbContext.EmployeePositions.Where(x => x.EmployeeId == command.EmployeeId && x.DepartmentPositionId == command.DepartmentPositionId)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result.Fail("Position not found")
                .WithName(nameof(command.EmployeeId))
                .WithCode("position_not_found")
                .WithStatus(HttpStatusCode.NotFound)
                .WithError("Position not found")
                .WithName(nameof(command.DepartmentPositionId))
                .WithCode("position_not_found");
        }
        return Result.Ok();
    }
}
