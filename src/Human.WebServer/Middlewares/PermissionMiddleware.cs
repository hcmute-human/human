using System.Security.Claims;
using FastEndpoints;
using FastEndpoints.Security;
using Human.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Human.WebServer.Middlewares;

public sealed class PermissionMiddleware
{
    private readonly RequestDelegate next;

    public PermissionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        var id = context.User.ClaimValue(ClaimTypes.NameIdentifier);
        if (id is null || !Guid.TryParseExact(id, "D", out var guid))
        {
            await next(context);
            return;
        }

        var permissions = await dbContext.UserPermissions
            .Where(x => x.User.Id == guid)
            .Select(x => x.Permission)
            .ToArrayAsync(context.RequestAborted)
            .ConfigureAwait(false);

        var claims = permissions.Select(x => new Claim("permissions", x));
        context.User.AddIdentity(new ClaimsIdentity(claims));
        await next(context);
    }
}