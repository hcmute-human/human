using NodaTime;

namespace Human.Domain.Models;

public record class UserRefreshToken : IAggregationRoot
{
    public User User { get; set; } = null!;
    public Instant CreatedTime { get; set; }
    public Guid Token { get; set; }
    public Instant ExpiryTime { get; set; }
}
