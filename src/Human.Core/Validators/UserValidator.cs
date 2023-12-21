using FastEndpoints;
using FluentValidation;
using Human.Domain.Models;

namespace Human.Core.Validators;

public sealed class UserValidator : Validator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.PasswordHash).NotEmpty();
    }
}
