using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.LeaveApplications.CreateLeaveApplication;

public sealed class CreateLeaveApplicationHandler : ICommandHandler<CreateLeaveApplicationCommand, Result<LeaveApplication>>
{
    private readonly IAppDbContext dbContext;

    public CreateLeaveApplicationHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

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

        var leaveApplication = command.ToLeaveApplication();
        dbContext.Add(leaveApplication);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return leaveApplication;
    }
}
