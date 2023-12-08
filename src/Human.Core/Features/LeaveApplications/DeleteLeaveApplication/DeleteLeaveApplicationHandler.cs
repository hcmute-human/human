using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.LeaveApplications.DeleteLeaveApplication;

public sealed class DeleteLeaveApplicationHandler : ICommandHandler<DeleteLeaveApplicationCommand, Result>
{
    private readonly IAppDbContext dbContext;

    public DeleteLeaveApplicationHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> ExecuteAsync(DeleteLeaveApplicationCommand command, CancellationToken ct)
    {
        var count = await dbContext.LeaveApplications
            .Where(x => x.Id == command.Id)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result.Fail("Leave application not found")
                .WithName(nameof(command.Id))
                .WithCode("leave_application_not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }
        return Result.Ok();
    }
}
