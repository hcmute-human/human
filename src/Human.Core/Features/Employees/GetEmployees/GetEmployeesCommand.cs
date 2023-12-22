using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.Employees.GetEmployees;

public sealed class GetEmployeesCommand : Collective, ICommand<Result<GetEmployeesResult>>
{
    public Guid? Id { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool CountOnly { get; set; }
}
