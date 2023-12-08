using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;

namespace Human.Core.Features.LeaveTypes.CreateLeaveType;

public sealed class CreateLeaveTypeHandler : ICommandHandler<CreateLeaveTypeCommand, Result<LeaveType>>
{
    private readonly IAppDbContext dbContext;

    public CreateLeaveTypeHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<LeaveType>> ExecuteAsync(CreateLeaveTypeCommand command, CancellationToken ct)
    {
        var leaveType = command.ToLeaveType();
        dbContext.Add(leaveType);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return leaveType;
    }
}
