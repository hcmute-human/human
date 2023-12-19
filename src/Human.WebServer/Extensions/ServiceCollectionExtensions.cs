using Human.Core.Constants;
using Human.Domain.Constants;
using Human.WebServer.Handlers;
using Human.WebServer.Handlers.LeaveApplications;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        var authorizationBuilder = services.AddAuthorizationBuilder();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.LeaveApplication.Read, x => x.Requirements.Add(new ReadLeaveApplicationRequirement(Permit.ReadLeaveApplication)));
        services.AddScoped<IAuthorizationHandler, IsLeaveApplicationIssuerAuthorizationHandler>();
        return services;
    }
}
