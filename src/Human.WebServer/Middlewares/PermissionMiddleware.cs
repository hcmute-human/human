using System.Security.Claims;
using FastEndpoints.Security;
using Human.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Human.WebServer.Middlewares;

public sealed class PermissionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate next = next;

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            await next(context).ConfigureAwait(false);
            return;
        }

        // if (context.GetEndpoint()?.Metadata.OfType<AuthorizeAttribute>().All(x => string.IsNullOrEmpty(x.Policy)) ?? true)
        // {
        //     await next(context).ConfigureAwait(false);
        //     return;
        // }

        var id = context.User.ClaimValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParseExact(id, "D", out var guid))
        {
            await next(context).ConfigureAwait(false);
            return;
        }

        var permissions = await dbContext.UserPermissions
            .Where(x => x.User.Id == guid)
            .Select(x => x.Permission)
            .ToArrayAsync(context.RequestAborted)
            .ConfigureAwait(false);

        var claims = permissions.Select(x => new Claim("permissions", x));
        context.User.AddIdentity(new ClaimsIdentity(claims));
        await next(context).ConfigureAwait(false);
    }
}
