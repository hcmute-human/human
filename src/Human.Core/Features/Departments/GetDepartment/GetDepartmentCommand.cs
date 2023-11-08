using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Departments.GetDepartment;

public class GetDepartmentCommand : ICommand<Result<GetDepartmentResult>>
{
    public required Guid Id { get; set; }
}
