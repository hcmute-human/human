using Human.Core.Features.Holidays.GetHolidays;
using Human.Core.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Holidays.GetHolidays;

internal sealed class Response : PaginatedList<Response.Item>
{
    public sealed class Item
    {
        public Guid Id { get; set; }
        public Instant CreatedTime { get; set; }
        public Instant UpdatedTime { get; set; }
        public string Name { get; set; } = string.Empty;
        public Instant StartTime { get; set; }
        public Instant EndTime { get; set; }
    }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetHolidaysResult result);
}
