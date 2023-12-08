using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Human.Core.Features.LeaveTypes.UpdateLeaveType;

public sealed class UpdateLeaveTypeHandler : ICommandHandler<UpdateLeaveTypeCommand, Result>
{
    private readonly IAppDbContext dbContext;

    public UpdateLeaveTypeHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> ExecuteAsync(UpdateLeaveTypeCommand command, CancellationToken ct)
    {
        var count = await dbContext.LeaveTypes
            .Where(x => x.Id == command.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.UpdatedTime, SystemClock.Instance.GetCurrentInstant())
                .SetProperty(x => x.Name, command.Name)
                .SetProperty(x => x.Description, command.Description)
                .SetProperty(x => x.Days, command.Days), ct)
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
