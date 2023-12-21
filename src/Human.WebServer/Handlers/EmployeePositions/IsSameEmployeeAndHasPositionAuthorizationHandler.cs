using System.Security.Claims;
using FastEndpoints.Security;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Human.WebServer.Handlers.EmployeePositions;

public class IsSameEmployeeAndHasPositionAuthorizationHandler(IAppDbContext dbContext) : IAuthorizationHandler
{
    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.Resource is not EmployeePosition employeePosition)
        {
            return;
        }

        bool? any = default;
        if (!Guid.TryParseExact(context.User.ClaimValue(ClaimTypes.NameIdentifier), "D", out var guid))
        {
            return;
        }
        foreach (var requirement in context.PendingRequirements)
        {
            if (requirement is not ReadRequirement)
            {
                continue;
            }

            any ??= (employeePosition.EmployeeId == guid, employeePosition.DepartmentPositionId == Guid.Empty) switch
            {
                (true, true) => true,
                (true, false) => await dbContext.EmployeePositions.AnyAsync(x => x.EmployeeId == guid && x.DepartmentPositionId == employeePosition.DepartmentPositionId).ConfigureAwait(false),
                _ => false,
            };
            if (any == true)
            {
                context.Succeed(requirement);
            }
        }
        return;
    }
}
