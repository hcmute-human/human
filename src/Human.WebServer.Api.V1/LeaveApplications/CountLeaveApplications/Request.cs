
using Riok.Mapperly.Abstractions;
using Human.Core.Features.LeaveApplications.GetLeaveApplications;
using Human.Core.Models;
using FastEndpoints;
using System.Security.Claims;
using Human.Domain.Constants;

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

    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid UserId { get; }
    [HasPermission(Permit.ReadEmployeePosition, IsRequired = false)]
    public bool HasReadPermission { get; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetLeaveApplicationsCommand ToCommand(this Request request);
}
