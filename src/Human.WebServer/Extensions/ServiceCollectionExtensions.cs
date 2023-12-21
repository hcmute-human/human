using Human.Core.Constants;
using Human.Domain.Constants;
using Human.WebServer.Handlers;
using Microsoft.AspNetCore.Authorization;
using LeaveApplications = Human.WebServer.Handlers.LeaveApplications;
using Users = Human.WebServer.Handlers.Users;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        var authorizationBuilder = services.AddAuthorizationBuilder();
        services.AddSingleton<IAuthorizationHandler, HasPermissionAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.Users.Read, x => x.Requirements.Add(new Users.UpdateRequirement(Permit.ReadUser)));
        authorizationBuilder.AddPolicy(AppPolicies.Users.Update, x => x.Requirements.Add(new Users.UpdateRequirement(Permit.UpdateUser)));
        services.AddSingleton<IAuthorizationHandler, Users.IsSameUserAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.LeaveApplications.Read, x => x.Requirements.Add(new LeaveApplications.ReadRequirement(Permit.ReadLeaveApplication)));
        services.AddScoped<IAuthorizationHandler, LeaveApplications.IsIssuerAuthorizationHandler>();
        return services;
    }
}
