using Riok.Mapperly.Abstractions;
using Human.Core.Features.LeaveApplications.GetLeaveApplication;
using FastEndpoints;
using Human.Domain.Constants;

namespace Human.WebServer.Api.V1.LeaveApplications.GetLeaveApplication;

internal sealed class Request
{
    public required Guid Id { get; set; }
    public bool? IncludeIssuer { get; set; }
    public bool? IncludeLeaveType { get; set; }
    public bool? IncludeProcessor { get; set; }

    [HasPermission(Permit.ReadLeaveApplication, IsRequired = false)]
    public bool HasReadPermission { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetLeaveApplicationCommand ToCommand(this Request request);
}
