using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Human.Core.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserPasswordResetToken> UserPasswordResetTokens { get; }
    EntityEntry Attach(object entity);
    EntityEntry Add(object entity);
    EntityEntry Update(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}
