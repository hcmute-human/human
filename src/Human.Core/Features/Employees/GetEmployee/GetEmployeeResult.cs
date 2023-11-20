using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Employees.GetEmployee;

public sealed class GetEmployeeResult
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Instant DateOfBirth { get; set; }
}

[Mapper]
internal static partial class GetEmployeeResultMapper
{
    [MapProperty(nameof(@Employee.User.Id), nameof(GetEmployeeResult.Id))]
    public static partial GetEmployeeResult ToResult(this Employee employee);
}