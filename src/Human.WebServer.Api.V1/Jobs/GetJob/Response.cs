using Riok.Mapperly.Abstractions;
using NodaTime;
using Human.Domain.Models;
using Human.Domain.Constants;

namespace Human.WebServer.Api.V1.Jobs.GetJob;

public sealed class Response
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

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this Job result);
}
