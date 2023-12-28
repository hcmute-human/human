using NodaTime;

namespace Human.Domain.Models;

public record class Test : IAggregationRoot
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public Guid CreatorId { get; set; }
    public Employee Creator { get; set; } = null!;
    public Guid JobId { get; set; }
    public Job Job { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ICollection<Question> Questions { get; set; } = null!;
}
