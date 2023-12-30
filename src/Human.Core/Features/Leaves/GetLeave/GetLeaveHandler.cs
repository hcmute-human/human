using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Human.Core.Features.Leaves.GetLeave;

public sealed class GetLeaveHandler(IAppDbContext dbContext) : ICommandHandler<GetLeaveCommand, Result<GetLeaveResult>>
{
    public async Task<Result<GetLeaveResult>> ExecuteAsync(GetLeaveCommand command, CancellationToken ct)
    {
        var query = dbContext.LeaveApplications.Where(x => x.Status == LeaveApplicationStatus.Approved && x.StartTime.InUtc().Year == (command.Year ?? SystemClock.Instance.GetCurrentInstant().InUtc().Year));
        var leaveTypesQuery = dbContext.LeaveTypes.AsQueryable();
        if (command.IssuerId is not null)
        {
            query = query.Where(x => x.IssuerId == command.IssuerId);
        }
        if (command.LeaveTypeId is not null)
        {
            query = query.Where(x => x.LeaveTypeId == command.LeaveTypeId);
            leaveTypesQuery = leaveTypesQuery.Where(x => x.Id == command.LeaveTypeId);
        }

        var leaveApplications = await query.ToArrayAsync(ct).ConfigureAwait(false);
        var totalDays = await leaveTypesQuery.SumAsync(x => x.Days, ct).ConfigureAwait(false);
        var totalUsedDays = leaveApplications.Sum(x => (float)(x.EndTime - x.StartTime).TotalDays);
        return new GetLeaveResult
        {
            TotalHours = totalDays * 8,
            UsedHours = totalUsedDays * 8,
        };
    }
}
