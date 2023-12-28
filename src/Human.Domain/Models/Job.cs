using Human.Domain.Constants;
using NodaTime;

namespace Human.Domain.Models;

public record class Job
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public Guid CreatorId { get; set; }
    public Employee Creator { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public JobStatus Status { get; set; }
    public Guid PositionId { get; set; }
    public DepartmentPosition Position { get; set; } = null!;
}
