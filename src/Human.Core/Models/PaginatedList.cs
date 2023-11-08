namespace Human.Core.Models;

public class PaginatedList<T>
{
    public required ICollection<T> Items { get; set; }
    public int TotalCount { get; set; }
}
