using Human.Domain.Constants;
using NodaTime;

namespace Human.Domain.Models;

public record class Employee : IAggregationRoot
{
    public Guid Id { get; set; }
    public User User { get; set; } = null!;
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Instant DateOfBirth { get; set; }
    public Gender Gender { get; set; }

    public ICollection<EmployeePosition> Positions { get; set; } = new HashSet<EmployeePosition>();
}
