using Microsoft.AspNetCore.Authorization;

namespace Human.WebServer.Handlers;

public class HasPermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.HasClaim(x => x.Type.Equals("permissions", StringComparison.Ordinal) && x.Value.Equals(requirement.Permission, StringComparison.Ordinal)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
