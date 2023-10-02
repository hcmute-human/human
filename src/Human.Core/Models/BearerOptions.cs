namespace Human.Core.Models;

public sealed class BearerOptions
{
    public const string Section = "Bearer";

    public required string Secret { get; set; }
    public required string ValidIssuer { get; set; }
    public required string[] ValidAudiences { get; set; }
}
