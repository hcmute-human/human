using NodaTime;

namespace Human.Domain.Models;

public record class Question : IAggregationRoot
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public Guid TestId { get; set; }
    public Test Test { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public ICollection<Choice>? Choices { get; set; }
}
