using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;

namespace Human.Core.Features.EmployeePositions.CreateEmployeePosition;

public sealed class CreateEmployeePositionHandler : ICommandHandler<CreateEmployeePositionCommand, Result<EmployeePosition>>
{
    private readonly IAppDbContext dbContext;

    public CreateEmployeePositionHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<EmployeePosition>> ExecuteAsync(CreateEmployeePositionCommand command, CancellationToken ct)
    {
        var position = command.ToEmployeePosition();
        dbContext.Add(position);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return position;
    }
}
