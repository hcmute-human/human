using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Departments.GetDepartment;

public sealed class GetDepartmentResult
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public required string Name { get; set; }
}

[Mapper]
internal static partial class GetDepartmentResultMapper
{
    public static partial GetDepartmentResult ToResult(this Department department);
}
