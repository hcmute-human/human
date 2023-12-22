using Human.Core.Constants;
using Human.Domain.Constants;
using Human.WebServer.Handlers;
using Microsoft.AspNetCore.Authorization;
using Users = Human.WebServer.Handlers.Users;
using UserPermissions = Human.WebServer.Handlers.UserPermissions;
using Departments = Human.WebServer.Handlers.Departments;
using DepartmentPositions = Human.WebServer.Handlers.DepartmentPositions;
using Employees = Human.WebServer.Handlers.Employees;
using EmployeePositions = Human.WebServer.Handlers.EmployeePositions;
using LeaveApplications = Human.WebServer.Handlers.LeaveApplications;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        var authorizationBuilder = services.AddAuthorizationBuilder();
        services.AddSingleton<IAuthorizationHandler, HasPermissionAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.Users.Read, x => x.Requirements.Add(new Users.ReadRequirement(Permit.ReadUser)));
        authorizationBuilder.AddPolicy(AppPolicies.Users.Update, x => x.Requirements.Add(new Users.UpdateRequirement(Permit.UpdateUser)));
        services.AddSingleton<IAuthorizationHandler, Users.IsSameUserAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.UserPermissions.Read, x => x.Requirements.Add(new UserPermissions.ReadRequirement(Permit.ReadUserPermission)));
        services.AddSingleton<IAuthorizationHandler, UserPermissions.IsSameUserAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.Employees.Read, x => x.Requirements.Add(new Employees.ReadRequirement(Permit.ReadEmployee)));
        services.AddSingleton<IAuthorizationHandler, Employees.IsSameEmployeeAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.EmployeePositions.Read, x => x.Requirements.Add(new EmployeePositions.ReadRequirement(Permit.ReadEmployeePosition)));
        services.AddScoped<IAuthorizationHandler, EmployeePositions.IsSameEmployeeAndHasPositionAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.Departments.Read, x => x.Requirements.Add(new Departments.ReadRequirement(Permit.ReadDepartment)));
        services.AddScoped<IAuthorizationHandler, Departments.IsEmployeeInDepartmentAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.DepartmentPositions.Read, x => x.Requirements.Add(new DepartmentPositions.ReadRequirement(Permit.ReadDepartmentPosition)));
        services.AddScoped<IAuthorizationHandler, DepartmentPositions.IsEmployeeInDepartmentAuthorizationHandler>();
        services.AddScoped<IAuthorizationHandler, DepartmentPositions.DoesEmployeeHasPositionAuthorizationHandler>();

        authorizationBuilder.AddPolicy(AppPolicies.LeaveApplications.Read, x => x.Requirements.Add(new LeaveApplications.ReadRequirement(Permit.ReadLeaveApplication)));
        services.AddScoped<IAuthorizationHandler, LeaveApplications.IsIssuerAuthorizationHandler>();
        return services;
    }
}
