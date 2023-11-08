using Human.Core.Features.Departments.DeleteDepartment;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Departments.DeleteDepartment;

internal sealed class Request
{
    public Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteDepartmentCommand ToCommand(this Request request);
}
