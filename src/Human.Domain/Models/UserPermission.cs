namespace Human.Domain.Models;

public record class UserPermission : IAggregationRoot
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string Permission { get; set; } = string.Empty;
}
