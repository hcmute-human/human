using Human.Core.Features.Departments.GetDepartment;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Departments.GetDepartment;

internal sealed class Response
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public required string Name { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetDepartmentResult result);
}
