using Human.Core.Features.Departments.GetDepartment;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Departments.GetDepartment;

internal sealed class Request
{
    public Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetDepartmentCommand ToCommand(this Request request);
}
