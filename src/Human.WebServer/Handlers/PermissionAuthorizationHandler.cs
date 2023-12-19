using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Human.WebServer.Handlers;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement, LeaveApplication>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement,
        LeaveApplication resource)
    {
        if (context.User.HasClaim(x => x.Type.Equals("permissions", StringComparison.Ordinal) && x.Value.Equals(requirement.Permission, StringComparison.Ordinal)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
