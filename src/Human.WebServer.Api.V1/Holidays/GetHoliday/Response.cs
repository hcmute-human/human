using Riok.Mapperly.Abstractions;
using NodaTime;
using Human.Domain.Models;

namespace Human.WebServer.Api.V1.Holidays.GetHoliday;

public sealed class Response
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public string Name { get; set; } = string.Empty;
    public Instant StartTime { get; set; }
    public Instant EndTime { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this Holiday result);
}
