using System.Security.Claims;
using FastEndpoints.Security;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Human.WebServer.Handlers.UserPermissions;

public class IsSameUserAuthorizationHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.Resource is not UserPermission userPermission)
        {
            return Task.CompletedTask;
        }
        foreach (var requirement in context.PendingRequirements)
        {
            if (requirement is not ReadRequirement)
            {
                continue;
            }
            if (Guid.TryParseExact(context.User.ClaimValue(ClaimTypes.NameIdentifier), "D", out var guid) && guid == userPermission.UserId)
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}
