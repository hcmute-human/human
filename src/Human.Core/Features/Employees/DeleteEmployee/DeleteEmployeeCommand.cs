using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Employees.DeleteEmployee;

public class DeleteEmployeeCommand : ICommand<Result>
{
    public required Guid Id { get; set; }
}
