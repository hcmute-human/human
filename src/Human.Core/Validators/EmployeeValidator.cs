using FastEndpoints;
using FluentValidation;
using Human.Domain.Models;

namespace Human.Core.Validators;

public sealed class EmployeeValidator : Validator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
    }
}
