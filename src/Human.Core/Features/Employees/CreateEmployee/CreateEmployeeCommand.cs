using FastEndpoints;
using FluentResults;
using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Employees.CreateEmployee;

public sealed class CreateEmployeeCommand : ICommand<Result<Employee>>
{
    public required User User { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Instant DateOfBirth { get; set; }
    public required EmploymentType EmploymentType { get; set; }
    public required decimal Salary { get; set; }
}

[Mapper]
internal static partial class CreateEmployeeCommandMapper
{
    public static partial Employee ToEmployee(this CreateEmployeeCommand command);
    public static Employee ToEmployee(this CreateEmployeeCommand command, User user)
    {
        return command.ToEmployee() with { User = user };
    }
}
