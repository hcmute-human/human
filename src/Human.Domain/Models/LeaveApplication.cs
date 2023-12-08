using Human.Domain.Constants;
using NodaTime;

namespace Human.Domain.Models;

public record class LeaveApplication : IAggregationRoot
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public Guid IssuerId { get; set; }
    public Employee Issuer { get; set; } = null!;
    public Guid LeaveTypeId { get; set; }
    public LeaveType LeaveType { get; set; } = null!;
    public Instant StartTime { get; set; }
    public Instant EndTime { get; set; }
    public LeaveApplicationStatus Status { get; set; }
    public string? Description { get; set; }
    public Guid? ProcessorId { get; set; }
    public Employee? Processor { get; set; }
}
