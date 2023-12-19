using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Human.Core.Features.LeaveApplications.UpdateLeaveApplication;

public sealed class UpdateLeaveApplicationHandler : ICommandHandler<UpdateLeaveApplicationCommand, Result<LeaveApplication>>
{
    private readonly IAppDbContext dbContext;

    public UpdateLeaveApplicationHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<LeaveApplication>> ExecuteAsync(UpdateLeaveApplicationCommand command, CancellationToken ct)
    {
        if (await dbContext.LeaveApplications.AnyAsync(x => x.Id != command.Id && x.IssuerId == command.IssuerId && x.StartTime <= command.EndTime && x.EndTime >= command.StartTime, ct).ConfigureAwait(false))
        {
            return Result
                .Fail("Time of leave already exists")
                .WithName(nameof(command.StartTime))
                .WithCode("duplicated_time")
                .WithStatus(HttpStatusCode.BadRequest)
                .WithError("Time of leave already exists")
                .WithName(nameof(command.EndTime))
                .WithCode("duplicated_time")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var count = await dbContext.LeaveApplications
            .Where(x => x.Id == command.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.UpdatedTime, SystemClock.Instance.GetCurrentInstant())
                .SetProperty(x => x.IssuerId, command.IssuerId)
                .SetProperty(x => x.LeaveTypeId, command.LeaveTypeId)
                .SetProperty(x => x.StartTime, command.StartTime)
                .SetProperty(x => x.EndTime, command.EndTime)
                .SetProperty(x => x.Status, command.Status)
                .SetProperty(x => x.Description, command.Description)
                .SetProperty(x => x.ProcessorId, command.ProcessorId), ct)
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
