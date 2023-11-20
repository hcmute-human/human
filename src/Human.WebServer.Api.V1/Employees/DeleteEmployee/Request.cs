using Human.Core.Features.Employees.DeleteEmployee;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Employees.DeleteEmployee;

internal sealed class Request
{
    public Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteEmployeeCommand ToCommand(this Request request);
}
