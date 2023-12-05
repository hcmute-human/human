using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.EmployeePositions.GetEmployeePosition;

internal sealed class Response
{
    public Guid EmployeeId { get; set; }
    public Guid DepartmentPositionId { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public Instant StartTime { get; set; }
    public Instant EndTime { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public decimal Salary { get; set; }
    public DepartmentPosition? DepartmentPosition { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this EmployeePosition result);
}
