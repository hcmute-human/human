using FastEndpoints;
using Human.Core.Features.LeaveApplications.GetLeaveApplications;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.LeaveApplications.GetLeaveApplications;

internal sealed class Request : Collective
{
    public string? IssuerName { get; set; }
    public string? AcquirerName { get; set; }
    public bool? IncludeIssuer { get; set; }
    public bool? IncludeLeaveType { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetLeaveApplicationsCommand ToCommand(this Request request);
}
