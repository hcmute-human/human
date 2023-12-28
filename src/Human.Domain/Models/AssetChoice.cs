namespace Human.Domain.Models;

public record class AssetChoice : Choice
{
    public AssetInfo Asset { get; set; } = null!;
}
