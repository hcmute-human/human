using Human.Domain.Constants;
using NodaTime;

namespace Human.Domain.Models;

public record class JobApplication
{
    public Guid Id { get; set; }
    public Instant CreatedTime { get; set; }
    public Instant UpdatedTime { get; set; }
    public Guid CandidateId { get; set; }
    public User Candidate { get; set; } = null!;
    public Guid JobId { get; set; }
    public Job Job { get; set; } = null!;
    public JobApplicationStatus Status { get; set; }
}
