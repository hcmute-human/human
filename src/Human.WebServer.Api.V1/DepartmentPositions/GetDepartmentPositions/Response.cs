using Human.Core.Features.DepartmentPositions.GetDepartmentPositions;
using Human.Core.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.DepartmentPositions.GetDepartmentPositions;

internal sealed class Response : PaginatedList<Response.Item>
{
    public sealed class Item
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public Instant CreatedTime { get; set; }
        public Instant UpdatedTime { get; set; }
        public required string Name { get; set; }
    }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetDepartmentPositionsResult result);
}
