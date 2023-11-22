using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;

namespace Human.Core.Features.DepartmentPositions.CreateDepartmentPosition;

public sealed class CreateDepartmentPositionHandler : ICommandHandler<CreateDepartmentPositionCommand, Result<DepartmentPosition>>
{
    private readonly IAppDbContext dbContext;

    public CreateDepartmentPositionHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<DepartmentPosition>> ExecuteAsync(CreateDepartmentPositionCommand command, CancellationToken ct)
    {
        var position = command.ToDepartmentPosition();
        dbContext.Add(position);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return position;
    }
}
