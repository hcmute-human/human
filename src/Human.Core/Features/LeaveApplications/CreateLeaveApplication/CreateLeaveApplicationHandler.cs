using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Human.Core.Features.LeaveApplications.CreateLeaveApplication;

public sealed class CreateLeaveApplicationHandler(IAppDbContext dbContext) : ICommandHandler<CreateLeaveApplicationCommand, Result<LeaveApplication>>
{
    public async Task<Result<LeaveApplication>> ExecuteAsync(CreateLeaveApplicationCommand command, CancellationToken ct)
    {
        if (!await dbContext.Employees.AnyAsync(x => x.Id == command.IssuerId, ct).ConfigureAwait(false))
        {
            return Result.Fail("Invalid issuer")
                .WithName(nameof(command.IssuerId))
                .WithCode("invalid_issuer")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        if (await dbContext.LeaveApplications.AnyAsync(x => x.IssuerId == command.IssuerId && x.StartTime <= command.EndTime && x.EndTime >= command.StartTime, ct).ConfigureAwait(false))
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

        var leaveType = await dbContext.LeaveTypes.FirstOrDefaultAsync(x => x.Id == command.LeaveTypeId, ct).ConfigureAwait(false);
        if (leaveType is null)
        {
            return Result.Fail("Leave type not found")
                .WithName(nameof(command.LeaveTypeId))
                .WithCode("not_found")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var leavingHours = (command.EndTime - command.StartTime).TotalDays * 8;
        var leftHours = await dbContext.LeaveApplications.Where(x => x.IssuerId == command.IssuerId && x.CreatedTime.InUtc().Year == SystemClock.Instance.GetCurrentInstant().InUtc().Year).SumAsync(x => (x.EndTime - x.StartTime).TotalDays, ct).ConfigureAwait(false) * 8;
        if (leavingHours + leftHours > leaveType.Days * 8)
        {
            return Result
                .Fail("Exceeding leave type days limit")
                .WithName(nameof(command.StartTime))
                .WithCode("exceeding_limit")
                .WithStatus(HttpStatusCode.BadRequest)
                .WithError("Exceeding leave type days limit")
                .WithName(nameof(command.EndTime))
                .WithCode("exceeding_limit")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var leaveApplication = command.ToLeaveApplication();
        dbContext.Add(leaveApplication);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return leaveApplication;
    }
}
