using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.Jobs.GetJobs;

public class GetJobsCommand : Collective, ICommand<Result<GetJobsResult>>
{
    public string? Title { get; set; }
    public bool? CountOnly { get; set; }
    public bool? IncludePosition { get; set; }
    public bool? IncludeDepartment { get; set; }
    public string? PositionName { get; set; }
    public string? DepartmentName { get; set; }
    public bool? ExcludeDescription { get; set; }
}
