
using Riok.Mapperly.Abstractions;
using Human.Core.Features.LeaveApplications.GetLeaveApplications;
using Human.Core.Models;

namespace Human.WebServer.Api.V1.LeaveApplications.CountLeaveApplications;

public sealed class Response : PaginatedList<object> { }

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetLeaveApplicationsResult result);
}
