using FastEndpoints;
using FluentResults;
using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;
using SystemTextJsonPatch;

namespace Human.Core.Features.Employees.PatchEmployee;

public sealed class PatchEmployeeCommand : ICommand<Result<Employee>>
{
    public Guid Id { get; set; }
    public required JsonPatchDocument<EmployeePatch> Patch { get; set; }
}

public sealed class EmployeePatch
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Instant DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}

[Mapper]
internal static partial class Mapper
{
    public static partial void ApplyTo(this EmployeePatch patch, Employee employee);
    public static partial EmployeePatch ToPatch(this Employee employee);
}
