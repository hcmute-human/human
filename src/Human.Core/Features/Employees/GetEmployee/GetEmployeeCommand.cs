using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Employees.GetEmployee;

public sealed class GetEmployeeCommand : ICommand<Result<GetEmployeeResult>>
{
    public required Guid Id { get; set; }
}
