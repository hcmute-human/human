using Human.Core.Features.LeaveApplications.DeleteLeaveApplication;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.LeaveApplications.DeleteLeaveApplication;

internal sealed class Request
{
    public Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteLeaveApplicationCommand ToCommand(this Request request);
}
