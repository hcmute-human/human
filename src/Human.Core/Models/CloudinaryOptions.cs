namespace Human.Core.Models;

public sealed class CloudinaryOptions
{
    public const string Section = "Cloudinary";

    public required string CloudName { get; set; }
    public required string ApiKey { get; set; }
    public required string ApiSecret { get; set; }
    public required string BasePath { get; set; }
}
