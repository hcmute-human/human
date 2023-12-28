namespace Human.Domain.Constants;

public enum JobApplicationStatus : byte
{
    None = 0,
    Pending,
    Reviewing,
    Approved,
    Rejected,
}
