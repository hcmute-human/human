using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.LeaveApplications.GetLeaveApplication;

public sealed class GetLeaveApplicationHandler(IAppDbContext dbContext) : ICommandHandler<GetLeaveApplicationCommand, Result<LeaveApplication>>
{
    private readonly IAppDbContext dbContext = dbContext;

    public async Task<Result<LeaveApplication>> ExecuteAsync(GetLeaveApplicationCommand command, CancellationToken ct)
    {
        var query = dbContext.LeaveApplications.Where(x => x.Id == command.Id);
        if (command.IncludeIssuer == true)
        {
            query = query.Include(x => x.Issuer);
        }
        if (command.IncludeLeaveType == true)
        {
            query = query.Include(x => x.LeaveType);
        }
        if (command.IncludeProcessor == true)
        {
            query = query.Include(x => x.Processor);
        }

        var leaveApplication = await query.FirstOrDefaultAsync(ct).ConfigureAwait(false);
        if (leaveApplication is null)
        {
            return Result.Fail("Leave application does not exist")
              .WithName(nameof(command.Id))
              .WithCode("not_found")
              .WithStatus(HttpStatusCode.NotFound);
        }
        return leaveApplication;
    }
}
