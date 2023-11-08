using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Departments.CreateDepartment;

public class CreateDepartmentCommand : ICommand<Result<CreateDepartmentResult>>
{
    public required string Name { get; set; }
}
