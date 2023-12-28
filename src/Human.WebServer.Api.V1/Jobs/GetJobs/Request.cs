using Human.Core.Features.Jobs.GetJobs;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Jobs.GetJobs;

internal sealed class Request : Collective
{
    public string? Title { get; set; }
    public bool? IncludePosition { get; set; }
    public bool? IncludeDepartment { get; set; }
    public string? PositionName { get; set; }
    public string? DepartmentName { get; set; }
    public bool? ExcludeDescription { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetJobsCommand ToCommand(this Request request);
}
