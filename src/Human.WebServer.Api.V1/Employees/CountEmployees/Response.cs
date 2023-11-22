
using Riok.Mapperly.Abstractions;
using Human.Core.Features.Employees.GetEmployees;
using NodaTime;
using Human.Core.Models;

namespace Human.WebServer.Api.V1.Employees.CountEmployees;

public sealed class Response : PaginatedList<Response.Item>
{
    public class Item
    {
        public Guid Id { get; set; }
        public Instant CreatedTime { get; set; }
        public Instant UpdatedTime { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Instant DateOfBirth { get; set; }
    }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetEmployeesResult result);
}
