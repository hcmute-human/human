using FastEndpoints;
using FluentResults;
using Human.Domain.Constants;

namespace Human.Core.Features.Permissions.GetPermissions;

public sealed class GetPermissionsHandler : ICommandHandler<GetPermissionsCommand, Result<GetPermissionsResult>>
{
    public Task<Result<GetPermissionsResult>> ExecuteAsync(GetPermissionsCommand command, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var query = Permit.AllPermissions.AsEnumerable();
        if (!string.IsNullOrEmpty(command.Name))
        {
            query = query.Where(x => x.Contains(command.Name, StringComparison.Ordinal));
        }

        var totalCount = query.Count();
        // query = command.Order.SortOrDefault(query, x => x.OrderBy(x => x.CreatedTime));
        var permissions = query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArray();
        return Task.FromResult(Result.Ok(new GetPermissionsResult
        {
            TotalCount = totalCount,
            Items = permissions,
        }));
    }
}
