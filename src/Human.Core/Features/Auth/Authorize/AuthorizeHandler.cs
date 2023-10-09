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
        var permissions = await dbContext.UserPermissions.Where(x => x.User.Id == command.UserId)
            .Select(x => x.Permission)
            .ToArrayAsync(cancellationToken: ct)
            .ConfigureAwait(false);

        var set = permissions.ToHashSet();
        if (!command.AllPermissions) return command.Permissions.Any(x => set.Contains(x));
        set.SymmetricExceptWith(command.Permissions);
        return set.Count == 0;
    }
}