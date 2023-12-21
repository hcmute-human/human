using System.Security.Claims;
using FastEndpoints.Security;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Human.WebServer.Handlers.Employees;

public class IsSameEmployeeAuthorizationHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.Resource is not Employee employee)
        {
            return Task.CompletedTask;
        }
        foreach (var requirement in context.PendingRequirements)
        {
            if (requirement is not ReadRequirement)
            {
                continue;
            }
            if (Guid.TryParseExact(context.User.ClaimValue(ClaimTypes.NameIdentifier), "D", out var guid) && guid == employee.Id)
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}
