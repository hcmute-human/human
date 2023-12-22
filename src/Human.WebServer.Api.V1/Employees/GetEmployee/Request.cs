using Riok.Mapperly.Abstractions;
using Human.Core.Features.Employees.GetEmployee;

namespace Human.WebServer.Api.V1.Employees.GetEmployee;

internal sealed class Request
{
    public required Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetEmployeeCommand ToCommand(this Request request);
}
