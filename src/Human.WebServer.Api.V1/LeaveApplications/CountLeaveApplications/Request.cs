
using Riok.Mapperly.Abstractions;
using Human.Core.Features.LeaveApplications.GetLeaveApplications;
using Human.Core.Models;

namespace Human.WebServer.Api.V1.LeaveApplications.CountLeaveApplications;

internal sealed class Request : Collective
{
    public Guid? IssuerId { get; set; }
    public string? IssuerName { get; set; }
    public string? AcquirerName { get; set; }
    public bool? IncludeIssuer { get; set; }
    public bool? IncludeLeaveType { get; set; }
    public Guid? DepartmentId { get; set; }
    public bool CountOnly => true;
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetLeaveApplicationsCommand ToCommand(this Request request);
}
