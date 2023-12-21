using FluentValidation;
using Human.Core.Interfaces;
using Human.Core.Services;
using Human.Core.Validators;
using Human.Domain.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection self)
    {
        self.AddSingleton<IJwtBearerService, JwtBearerService>();
        self.AddSingleton<IEmailTemplateRenderer, HtmlEmailTemplateRenderer>();
        self.AddSingleton<IValidator<User>, UserValidator>();
        self.AddSingleton<IValidator<Department>, DepartmentValidator>();
        self.AddSingleton<IValidator<Employee>, EmployeeValidator>();
        self.AddSingleton<IValidator<LeaveApplication>, LeaveApplicationValidator>();
        return self;
    }
}
