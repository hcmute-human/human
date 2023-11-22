using Human.Domain.Constants;
using NodaTime;

namespace Human.Domain.Models;

public record class EmployeePosition : IAggregationRoot
{
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public Guid DepartmentPositionId { get; set; }
    public DepartmentPosition DepartmentPosition { get; set; } = null!;
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public Instant StartTime { get; set; }
    public Instant EndTime { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public decimal Salary { get; set; }
}
