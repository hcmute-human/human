
using Riok.Mapperly.Abstractions;
using NodaTime;
using Human.Domain.Models;
using Human.Domain.Constants;

namespace Human.WebServer.Api.V1.Employees.GetEmployee;

public sealed class Response
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Instant DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this Employee result);
}
