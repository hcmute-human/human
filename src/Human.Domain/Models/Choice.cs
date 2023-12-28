namespace Human.Domain.Models;

public abstract record class Choice : IAggregationRoot
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}
