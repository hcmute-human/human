using Human.Core.Features.Departments.CreateDepartment;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Departments.CreateDepartment;

internal sealed class Response
{
    public Guid Id { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    [MapperIgnoreSource(nameof(CreateDepartmentResult.CreatedTime))]
    public static partial Response ToResponse(this CreateDepartmentResult result);
}
