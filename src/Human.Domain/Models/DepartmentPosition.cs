using NodaTime;

namespace Human.Domain.Models;

public record class DepartmentPosition : IAggregationRoot
{
    public Guid Id { get; set; }
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<EmployeePosition> EmployeePositions { get; set; } = null!;
}
