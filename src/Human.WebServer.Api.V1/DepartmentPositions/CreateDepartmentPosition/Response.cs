using Human.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.DepartmentPositions.CreateDepartmentPosition;

internal sealed class Response
{
    public Guid Id { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this DepartmentPosition result);
}
