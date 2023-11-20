
using Riok.Mapperly.Abstractions;
using Human.Domain.Models;

namespace Human.WebServer.Api.V1.Employees.CreateEmployee;

public sealed class Response
{
    public Guid Id { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this Employee employee);
}
