using CloudinaryDotNet;

namespace Human.Infrastructure.Models;

public class SignedUrl
{
    public required Url Url { get; set; }
    public required SortedDictionary<string, object> Parameters { get; set; }
    public required string Signature { get; set; }
}
