using NodaTime;

namespace Human.Domain.Models;

public record class User : IAggregationRoot
{
    public Guid Id { get; set; }
    public Instant CreationTime { get; set; }
    public Instant UpdatingTime { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}
