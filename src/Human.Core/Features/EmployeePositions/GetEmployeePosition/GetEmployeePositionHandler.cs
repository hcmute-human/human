using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.EmployeePositions.GetEmployeePosition;

public sealed class GetEmployeePositionHandler : ICommandHandler<GetEmployeePositionCommand, Result<EmployeePosition>>
{
    private readonly IAppDbContext dbContext;

    public GetEmployeePositionHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<EmployeePosition>> ExecuteAsync(GetEmployeePositionCommand command, CancellationToken ct)
    {
        var query = dbContext.EmployeePositions.Where(x => x.EmployeeId == command.EmployeeId && x.DepartmentPositionId == command.DepartmentPositionId);
        query = (command.IncludeDepartment, command.IncludeDepartmentPosition) switch
        {
            (true, true) or (true, false) => query.Include(x => x.DepartmentPosition).ThenInclude(x => x.Department),
            (false, true) => query.Include(x => x.DepartmentPosition),
            _ => query,
        };

        var position = await query
            .FirstOrDefaultAsync(ct).ConfigureAwait(false);
        if (position is null)
        {
            return Result.Fail("Position not found")
                .WithName(nameof(command.EmployeeId))
                .WithCode("position_not_found")
                .WithStatus(HttpStatusCode.NotFound)
                .WithError("Position not found")
                .WithName(nameof(command.DepartmentPositionId))
                .WithCode("position_not_found");
        }
        return position;
    }
}
