namespace Human.Domain.Models;

public record class UserPermission : IAggregationRoot
{
    public User User { get; set; } = null!;
    public string Permission { get; set; } = string.Empty;
}
