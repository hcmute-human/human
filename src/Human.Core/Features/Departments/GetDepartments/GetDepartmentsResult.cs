using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Departments.GetDepartments;

public sealed class GetDepartmentsResult
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public required string Name { get; set; }
}

[Mapper]
internal static partial class GetDepartmentsResultMapper
{
    public static partial GetDepartmentsResult[] ToResult(this Department[] department);
}
