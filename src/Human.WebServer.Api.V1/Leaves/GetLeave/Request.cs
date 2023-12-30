using System.Security.Claims;
using FastEndpoints;
using Human.Core.Features.Leaves.GetLeave;
using Human.Core.Models;
using Human.Domain.Constants;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Leaves.GetLeave;

internal sealed class Request : Collective
{
    public Guid? LeaveTypeId { get; set; }
    public Guid? IssuerId { get; set; }
    public int? Year { get; set; }

    [HasPermission(Permit.ReadLeaveApplication, IsRequired = false)]
    public bool HasReadPermission { get; set; }
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid UserId { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetLeaveCommand ToCommand(this Request request);
}
