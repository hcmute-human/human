using Riok.Mapperly.Abstractions;
using NodaTime;
using Human.Domain.Models;

namespace Human.WebServer.Api.V1.Users.GetUser;

public sealed class Response
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public string Email { get; set; } = null!;
    public AssetInfo? Avatar { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this User result);
}
