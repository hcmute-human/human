using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Departments.DeleteDepartment;

public class DeleteDepartmentCommand : ICommand<Result>
{
    public required Guid Id { get; set; }
}
