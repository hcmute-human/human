using NodaTime;

namespace Human.Domain.Models;

public record class UserPasswordResetToken : IAggregationRoot
{
    public string Token { get; set; } = null!;
    public User User { get; set; } = null!;
    public Instant ExpirationTime { get; set; }
}
