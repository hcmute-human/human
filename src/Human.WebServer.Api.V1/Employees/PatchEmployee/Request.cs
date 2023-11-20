using Human.Core.Features.Employees.PatchEmployee;
using Riok.Mapperly.Abstractions;
using SystemTextJsonPatch;

namespace Human.WebServer.Api.V1.Employees.PatchEmployee;

internal sealed class Request
{
    public Guid Id { get; set; }
    public required JsonPatchDocument<EmployeePatch> Patch { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial PatchEmployeeCommand ToCommand(this Request request);
}
