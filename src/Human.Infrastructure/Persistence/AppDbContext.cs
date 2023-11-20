using Human.Core.Interfaces;
using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<UserPasswordResetToken> UserPasswordResetTokens => Set<UserPasswordResetToken>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
    public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();
    public DbSet<Department> Departments => Set<Department>();

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("admin")
        };
        modelBuilder.Entity<User>().HasData(user);
        modelBuilder.Entity<UserPermission>().HasData(
            new[] { Permit.CreateDepartment, Permit.DeleteDepartment, Permit.ReadDepartment, Permit.UpdateDepartment, Permit.CreateEmployee, Permit.DeleteEmployee, Permit.ReadEmployee, Permit.UpdateEmployee }
                .Select(x => new
                {
                    UserId = user.Id,
                    Permission = x
                }));
    }
}
