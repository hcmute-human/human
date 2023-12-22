
using Riok.Mapperly.Abstractions;
using Human.Core.Features.Employees.GetEmployees;
using Human.Core.Models;
using FastEndpoints;
using Human.Domain.Constants;
using System.Security.Claims;

namespace Human.WebServer.Api.V1.Employees.GetEmployees;

internal sealed class Request : Collective
{
    public Guid? DepartmentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid EmployeeId { get; set; }
    [HasPermission(Permit.ReadEmployee, IsRequired = false)]
    public bool HasReadPermission { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetEmployeesCommand ToCommand(this Request request);
}
