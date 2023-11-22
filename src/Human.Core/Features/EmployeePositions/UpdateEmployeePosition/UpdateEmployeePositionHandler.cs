using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;

namespace Human.Core.Features.EmployeePositions.UpdateEmployeePosition;

public sealed class UpdateEmployeePositionHandler : ICommandHandler<UpdateEmployeePositionCommand, Result<EmployeePosition>>
{
    private readonly IAppDbContext dbContext;

    public UpdateEmployeePositionHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<EmployeePosition>> ExecuteAsync(UpdateEmployeePositionCommand command, CancellationToken ct)
    {
        var position = command.ToEmployeePosition();
        dbContext.Update(position);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return position;
    }
}
