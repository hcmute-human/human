using Human.Core.Constants;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Models;

public class Orderable
{
    private string UppercasedName => string.Concat(Name[0].ToString().ToUpperInvariant(), Name.AsSpan(1));

    public required string Name { get; set; }
    public Order Order { get; set; } = Order.Ascending;

    public IOrderedQueryable<T> Sort<T>(IQueryable<T> query) where T : notnull
    {
        return Order == Order.Ascending
            ? query.OrderBy(x => EF.Property<T>(x, UppercasedName))
            : query.OrderByDescending(x => EF.Property<T>(x, UppercasedName));
    }

    public IOrderedQueryable<T> Sort<T>(IOrderedQueryable<T> query) where T : notnull
    {
        return Order == Order.Ascending
            ? query.ThenBy(x => EF.Property<T>(x, UppercasedName))
            : query.ThenByDescending(x => EF.Property<T>(x, UppercasedName));
    }
}
