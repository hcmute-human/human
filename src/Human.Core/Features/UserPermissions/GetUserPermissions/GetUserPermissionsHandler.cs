using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.UserPermissions.GetUserPermissions;

public sealed class GetUserPermissionsHandler(IAppDbContext dbContext) : ICommandHandler<GetUserPermissionsCommand, Result<GetUserPermissionsResult>>
{
    private readonly IAppDbContext dbContext = dbContext;

    public async Task<Result<GetUserPermissionsResult>> ExecuteAsync(GetUserPermissionsCommand command, CancellationToken ct)
    {
        var query = dbContext.UserPermissions.Where(x => x.User.Id == command.UserId);
        if (!string.IsNullOrEmpty(command.Permission))
        {
            query = query.Where(x => x.Permission.Contains(command.Permission));
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        query = command.Order.SortOrDefault(query, x => x.OrderBy(x => x.Permission));

        var userPermissions = await query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct).ConfigureAwait(false);
        return new GetUserPermissionsResult
        {
            TotalCount = totalCount,
            Items = userPermissions,
        };
    }
}
