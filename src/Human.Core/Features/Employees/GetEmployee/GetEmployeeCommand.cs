using FastEndpoints;
using FluentResults;
using Human.Domain.Models;

namespace Human.Core.Features.Employees.GetEmployee;

public sealed class GetEmployeeCommand : ICommand<Result<Employee>>
{
    public required Guid Id { get; set; }
}
