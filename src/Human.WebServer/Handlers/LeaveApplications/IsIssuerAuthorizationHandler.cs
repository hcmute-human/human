using System.Security.Claims;
using FastEndpoints.Security;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Human.WebServer.Handlers.LeaveApplications;

public class IsIssuerAuthorizationHandler(IAppDbContext dbContext) : IAuthorizationHandler
{
    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.Resource is not LeaveApplication leaveApplication)
        {
            return;
        }

        bool? any = default;
        foreach (var requirement in context.PendingRequirements)
        {
            if (requirement is not ReadRequirement)
            {
                continue;
            }

            any ??= Guid.TryParseExact(context.User.ClaimValue(ClaimTypes.NameIdentifier), "D", out var guid)
                && await dbContext.LeaveApplications.AnyAsync(x => x.Id == leaveApplication.Id && x.IssuerId == guid).ConfigureAwait(false);
            if (any == true)
            {
                context.Succeed(requirement);
            }
        }
    }
}
