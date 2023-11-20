
using Riok.Mapperly.Abstractions;
using Human.Core.Features.Employees.GetEmployee;
using NodaTime;

namespace Human.WebServer.Api.V1.Employees.GetEmployee;

public sealed class Response
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Instant DateOfBirth { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetEmployeeResult result);
}