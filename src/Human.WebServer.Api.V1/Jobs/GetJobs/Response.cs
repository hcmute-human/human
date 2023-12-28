using Human.Core.Features.Jobs.GetJobs;
using Human.Core.Models;
using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Jobs.GetJobs;

internal sealed class Response : PaginatedList<Response.Item>
{
    public sealed class Item
    {
        public Guid Id { get; set; }
        public Instant CreatedTime { get; set; }
        public Instant UpdatedTime { get; set; }
        public Guid CreatorId { get; set; }
        public Employee? Creator { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public JobStatus Status { get; set; }
        public Guid PositionId { get; set; }
        public DepartmentPosition? Position { get; set; }
    }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetJobsResult result);
}
