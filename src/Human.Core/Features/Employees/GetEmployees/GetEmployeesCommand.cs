using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.Employees.GetEmployees;

public sealed class GetEmployeesCommand : Collective, ICommand<Result<GetEmployeesResult>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
