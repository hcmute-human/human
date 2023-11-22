using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.DepartmentPositions.DeleteDepartmentPosition;

public sealed class DeleteDepartmentPositionHandler : ICommandHandler<DeleteDepartmentPositionCommand, Result>
{
    private readonly IAppDbContext dbContext;

    public DeleteDepartmentPositionHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> ExecuteAsync(DeleteDepartmentPositionCommand command, CancellationToken ct)
    {
        var count = await dbContext.DepartmentPositions
            .Where(x => x.Id == command.Id)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result.Fail("Position not found")
                .WithName(nameof(command.Id))
                .WithCode("position_not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }
        return Result.Ok();
    }
}
