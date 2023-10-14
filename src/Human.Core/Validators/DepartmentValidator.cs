using FastEndpoints;
using FluentValidation;
using Human.Domain.Models;

namespace Human.Core.Validators;

public sealed class DepartmentValidator : Validator<Department>
{
    public DepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}
