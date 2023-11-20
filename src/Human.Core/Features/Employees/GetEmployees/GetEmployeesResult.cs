using Human.Core.Models;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Employees.GetEmployees;

public sealed class GetEmployeesResult : PaginatedList<GetEmployeesResult.Item>
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
internal static partial class GetEmployeesResultMapper
{
    public static partial GetEmployeesResult.Item ToItem(this Employee employee);

    public static partial GetEmployeesResult.Item[] ToItems(this Employee[] employees);
}
