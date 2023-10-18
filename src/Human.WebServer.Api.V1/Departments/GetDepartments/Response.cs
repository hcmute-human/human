using Human.Core.Features.Departments.GetDepartments;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Departments.GetDepartments;

internal sealed class Response
{
    public int TotalCount { get; set; }
    public Item[] Items { get; set; } = Array.Empty<Item>();

    public sealed class Item
    {
        public Guid Id { get; set; }
        public Instant CreatedTime { get; set; }
        public Instant UpdatedTime { get; set; }
        public required string Name { get; set; }
    }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetDepartmentsResult result);
}
