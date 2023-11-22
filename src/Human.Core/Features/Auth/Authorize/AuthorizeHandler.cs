using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Auth.Authorize;

public sealed class AuthorizeHandler : ICommandHandler<AuthorizeCommand, Result<bool>>
{
    private readonly IAppDbContext dbContext;

    public AuthorizeHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<bool>> ExecuteAsync(AuthorizeCommand command, CancellationToken ct)
    {
        var permissions = await dbContext.UserPermissions.Where(x => x.User.Id == command.UserId && command.Permissions.Contains(x.Permission))
            .Select(x => x.Permission)
            .ToArrayAsync(cancellationToken: ct)
            .ConfigureAwait(false);
        if (!command.AllPermissions) return permissions.Length > 0;
        return permissions.Length == command.Permissions.Count;
    }
}
