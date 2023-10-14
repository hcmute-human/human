using Human.Core.Features.Departments.PatchDepartment;
using Riok.Mapperly.Abstractions;
using SystemTextJsonPatch;

namespace Human.WebServer.Api.V1.Departments.PatchDepartment;

internal sealed class Request
{
    public Guid Id { get; set; }
    public required JsonPatchDocument<DepartmentPatch> Patch { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial PatchDepartmentCommand ToCommand(this Request request);
}
