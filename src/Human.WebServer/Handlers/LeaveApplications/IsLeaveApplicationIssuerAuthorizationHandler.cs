using System.Security.Claims;
using FastEndpoints.Security;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Human.WebServer.Handlers.LeaveApplications;

public class IsLeaveApplicationIssuerAuthorizationHandler(IAppDbContext dbContext) :
    AuthorizationHandler<ReadLeaveApplicationRequirement, LeaveApplication>
{
    private readonly IAppDbContext dbContext = dbContext;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ReadLeaveApplicationRequirement requirement,
        LeaveApplication resource)
    {
        if (Guid.TryParseExact(context.User.ClaimValue(ClaimTypes.NameIdentifier), "D", out var guid)
            && await dbContext.LeaveApplications.AnyAsync(x => x.Id == resource.Id && x.IssuerId == guid).ConfigureAwait(false))
        {
            context.Succeed(requirement);
        }
    }
}
