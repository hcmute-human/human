using FastEndpoints;
using FluentResults;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;
using SystemTextJsonPatch;

namespace Human.Core.Features.Departments.PatchDepartment;

public sealed class PatchDepartmentCommand : ICommand<Result<Department>>
{
    public Guid Id { get; set; }
    public required JsonPatchDocument<DepartmentPatch> Patch { get; set; }
}

public sealed class DepartmentPatch
{
    public required string Name { get; set; }
}

[Mapper]
internal static partial class Mapper
{
    public static partial void ApplyTo(this DepartmentPatch patch, Department department);
}
