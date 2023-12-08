using Human.Core.Features.LeaveTypes.GetLeaveTypes;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.LeaveTypes.GetLeaveTypes;

internal sealed class Request : Collective
{
    public string? Name { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetLeaveTypesCommand ToCommand(this Request request);
}
