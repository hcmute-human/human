
using FastEndpoints;
using FluentValidation;
using NodaTime;
using Human.Core.Features.Employees.CreateEmployee;
using Human.Domain.Constants;
using System.Globalization;
using Human.Domain.Models;

namespace Human.WebServer.Api.V1.Employees.CreateEmployee;

internal sealed class Request
{
    public Guid? UserId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? DateOfBirth { get; set; }
    public EmploymentType? EmploymentType { get; set; }
    public decimal? Salary { get; set; }
}

internal sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.UserId).NotNull().When(x => string.IsNullOrEmpty(x.Email) && string.IsNullOrEmpty(x.Password));
        RuleFor(x => x.Email).NotNull().EmailAddress().When(x => x.UserId is null);
        RuleFor(x => x.Password).NotNull().MinimumLength(7).When(x => x.UserId is null);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotNull();
        RuleFor(x => x.EmploymentType).NotNull();
        RuleFor(x => x.Salary)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}

internal static class RequestMapper
{
    public static CreateEmployeeCommand ToCommand(this Request request, User user)
    {
        return new CreateEmployeeCommand
        {
            User = user,
            FirstName = request.FirstName!,
            LastName = request.LastName!,
            EmploymentType = request.EmploymentType!.Value,
            DateOfBirth = Instant.FromDateTimeOffset(DateTimeOffset.Parse(request.DateOfBirth!, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)),
            Salary = request.Salary!.Value
        };
    }
}
