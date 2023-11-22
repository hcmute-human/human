
using Riok.Mapperly.Abstractions;
using Human.Core.Features.Employees.GetEmployees;
using Human.Core.Models;

namespace Human.WebServer.Api.V1.Employees.CountEmployees;

internal sealed class Request : Collective
{
    public Guid? DepartmentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool CountOnly => true;
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetEmployeesCommand ToCommand(this Request request);
}
