using System.Security.Claims;
using FastEndpoints.Security;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Human.WebServer.Handlers.Users;

public class IsSameUserAuthorizationHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.Resource is not User user)
        {
            return Task.CompletedTask;
        }
        foreach (var requirement in context.PendingRequirements)
        {
            if (requirement is not ReadRequirement && requirement is not UpdateRequirement)
            {
                continue;
            }
            if (Guid.TryParseExact(context.User.ClaimValue(ClaimTypes.NameIdentifier), "D", out var guid) && guid == user.Id)
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}
