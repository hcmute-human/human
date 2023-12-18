using Human.Core.Features.LeaveApplications.GetLeaveApplications;
using Human.Core.Models;
using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.LeaveApplications.GetLeaveApplications;

internal sealed class Response : PaginatedList<Response.Item>
{
    public sealed class Item
    {
        public Guid Id { get; set; }
        public Instant CreatedTime { get; set; }
        public Instant UpdatedTime { get; set; }
        public Guid IssuerId { get; set; }
        public Employee? Issuer { get; set; }
        public Guid LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }
        public Instant StartTime { get; set; }
        public Instant EndTime { get; set; }
        public LeaveApplicationStatus Status { get; set; }
        public string? Description { get; set; }
        public Guid? ProcessorId { get; set; }
        public Employee? Processor { get; set; }
    }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetLeaveApplicationsResult result);
}
