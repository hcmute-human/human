using System.Security.Claims;
using FastEndpoints;
using Human.Core.Features.Departments.GetDepartments;
using Human.Core.Models;
using Human.Domain.Constants;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Departments.GetDepartments;

internal sealed class Request : Collective
{
    public string? Name { get; set; }

    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid UserId { get; set; }
    [HasPermission(Permit.ReadDepartment, IsRequired = false)]
    public bool HasReadPermission { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetDepartmentsCommand ToCommand(this Request request);
}
