using FastEndpoints;
using FluentValidation;
using Human.Domain.Models;

namespace Human.Core.Validators;

public sealed class LeaveApplicationValidator : Validator<LeaveApplication>
{
    public LeaveApplicationValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.CreatedTime).NotNull();
        RuleFor(x => x.UpdatedTime).NotNull();
        RuleFor(x => x.IssuerId).NotNull();
        RuleFor(x => x.LeaveTypeId).NotNull();
        RuleFor(x => x.StartTime).NotNull();
        RuleFor(x => x.EndTime).NotNull();
    }
}
