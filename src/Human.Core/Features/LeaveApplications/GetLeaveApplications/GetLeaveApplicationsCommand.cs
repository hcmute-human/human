using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.LeaveApplications.GetLeaveApplications;

public class GetLeaveApplicationsCommand : Collective, ICommand<Result<GetLeaveApplicationsResult>>
{
    public Guid? IssuerId { get; set; }
    public string? IssuerName { get; set; }
    public string? ProcessorName { get; set; }
    public bool? IncludeIssuer { get; set; }
    public bool? IncludeLeaveType { get; set; }
    public Guid? DepartmentId { get; set; }
    public bool? CountOnly { get; set; }
}
