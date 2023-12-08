using Human.Core.Features.LeaveTypes.GetLeaveTypes;
using Human.Core.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.LeaveTypes.GetLeaveTypes;

internal sealed class Response : PaginatedList<Response.Item>
{
    public sealed class Item
    {
        public Guid Id { get; set; }
        public Instant CreatedTime { get; set; }
        public Instant UpdatedTime { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Days { get; set; }
    }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetLeaveTypesResult result);
}
