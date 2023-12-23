using NodaTime;

namespace Human.Domain.Models;

public record class Holiday : IAggregationRoot
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public string Name { get; set; } = string.Empty;
    public Instant StartTime { get; set; }
    public Instant EndTime { get; set; }
}
