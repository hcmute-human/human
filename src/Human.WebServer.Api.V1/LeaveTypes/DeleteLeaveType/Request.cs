using Human.Core.Features.LeaveTypes.DeleteLeaveType;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.LeaveTypes.DeleteLeaveType;

internal sealed class Request
{
    public Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteLeaveTypeCommand ToCommand(this Request request);
}
