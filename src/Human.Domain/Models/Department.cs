using NodaTime;

namespace Human.Domain.Models;

public record class Department : IAggregationRoot
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public string Name { get; set; } = null!;
}