using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
}
