using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.LeaveTypes.DeleteLeaveType;

public sealed class DeleteLeaveTypeHandler : ICommandHandler<DeleteLeaveTypeCommand, Result>
{
    private readonly IAppDbContext dbContext;

    public DeleteLeaveTypeHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> ExecuteAsync(DeleteLeaveTypeCommand command, CancellationToken ct)
    {
        var count = await dbContext.LeaveTypes
            .Where(x => x.Id == command.Id)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result.Fail("Leave type not found")
                .WithName(nameof(command.Id))
                .WithCode("leave_type_not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }
        return Result.Ok();
    }
}
