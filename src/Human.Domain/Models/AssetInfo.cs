namespace Human.Domain.Models;

public class AssetInfo
{
    public string Key { get; set; } = null!;
    public string Format { get; set; } = null!;
    public long Version { get; set; }
}
