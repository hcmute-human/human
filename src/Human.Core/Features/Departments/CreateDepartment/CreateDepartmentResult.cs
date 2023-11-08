using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Departments.CreateDepartment;

public class CreateDepartmentResult
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
}

[Mapper]
internal static partial class CreateDepartmentResultMapper
{
    [MapperIgnoreSource(nameof(Department.UpdatedTime))]
    [MapperIgnoreSource(nameof(Department.Name))]
    public static partial CreateDepartmentResult ToResult(this Department department);
}
