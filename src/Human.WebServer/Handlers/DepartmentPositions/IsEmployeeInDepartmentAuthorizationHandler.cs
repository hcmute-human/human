using System.Security.Claims;
using FastEndpoints.Security;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Human.WebServer.Handlers.DepartmentPositions;

public class IsEmployeeInDepartmentAuthorizationHandler(IAppDbContext dbContext) : IAuthorizationHandler
{
    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.Resource is not DepartmentPosition departmentPosition)
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
                && await dbContext.EmployeePositions.AnyAsync(x => x.EmployeeId == guid && x.DepartmentPosition.DepartmentId == departmentPosition.DepartmentId).ConfigureAwait(false);
            if (any == true)
            {
                context.Succeed(requirement);
            }
        }
        return;
    }
}
