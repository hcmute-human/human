using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Human.WebServer.Handlers;

public class ReadLeaveApplicationAuthorizationHandler :
    AuthorizationHandler<ReadLeaveApplicationAuthorizationRequirement, LeaveApplication>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ReadLeaveApplicationAuthorizationRequirement requirement,
        LeaveApplication resource)
    {
        if (context.User.HasClaim(x => x.Type.Equals("permissions", StringComparison.Ordinal) && x.Value.Equals(Permit.ReadLeaveApplication, StringComparison.Ordinal)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
