using System.Net;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using SystemTextJsonPatch.Exceptions;

namespace Human.Core.Features.LeaveApplications.PatchLeaveApplication;

public sealed class PatchLeaveApplicationHandler(IAppDbContext dbContext, IValidator<LeaveApplication> validator) : ICommandHandler<PatchLeaveApplicationCommand, Result<LeaveApplication>>
{
    private readonly IAppDbContext dbContext = dbContext;
    private readonly IValidator<LeaveApplication> validator = validator;

    public async Task<Result<LeaveApplication>> ExecuteAsync(PatchLeaveApplicationCommand command, CancellationToken ct)
    {
        var leaveApplication = await dbContext.LeaveApplications
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct)
            .ConfigureAwait(false);

        if (leaveApplication is null)
        {
            return Result.Fail("Leave application not found")
                .WithName(nameof(command.Id))
                .WithCode("not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }

        try
        {
            command.Patch.ApplyTo(leaveApplication);
        }
        catch (JsonPatchException e)
        {
            return Result.Fail(e.Message)
                .WithName(nameof(command.Patch))
                .WithCode("patch_failed")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var validation = await validator.ValidateAsync(leaveApplication, ct).ConfigureAwait(false);
        if (!validation.IsValid)
        {
            return Result.Fail(validation.Errors.Select(x => new Error(x.ErrorMessage)
                .WithName(x.PropertyName)
                .WithCode(x.ErrorCode)
                .WithStatus(HttpStatusCode.BadRequest)));
        }

        if (await dbContext.LeaveApplications.AnyAsync(x => x.IssuerId == leaveApplication.IssuerId && x.StartTime <= leaveApplication.EndTime && x.EndTime >= leaveApplication.StartTime, ct).ConfigureAwait(false))
        {
            return Result
                .Fail("Time of leave already exists")
                .WithName(nameof(leaveApplication.StartTime))
                .WithCode("duplicated_time")
                .WithStatus(HttpStatusCode.BadRequest)
                .WithError("Time of leave already exists")
                .WithName(nameof(leaveApplication.EndTime))
                .WithCode("duplicated_time")
                .WithStatus(HttpStatusCode.BadRequest);
        }
        leaveApplication.UpdatedTime = SystemClock.Instance.GetCurrentInstant();
        dbContext.Update(leaveApplication);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return leaveApplication;
    }
}
