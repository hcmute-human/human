using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.Departments.GetDepartments;

public class GetDepartmentsCommand : Collective, ICommand<Result<GetDepartmentsResult>>
{
    public string? Name { get; set; }
}
