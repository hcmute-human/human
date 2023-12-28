using System.Reflection;
using Human.Core.Interfaces;
using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options), IAppDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<UserPasswordResetToken> UserPasswordResetTokens => Set<UserPasswordResetToken>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
    public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<DepartmentPosition> DepartmentPositions => Set<DepartmentPosition>();
    public DbSet<EmployeePosition> EmployeePositions => Set<EmployeePosition>();
    public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
    public DbSet<LeaveApplication> LeaveApplications => Set<LeaveApplication>();
    public DbSet<Holiday> Holidays => Set<Holiday>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<Question> Questions => Set<Question>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("admin"),
        };
        modelBuilder.Entity<User>().HasData(user);
        modelBuilder.Entity<UserPermission>().HasData(
            typeof(Permit).GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => new
            {
                UserId = user.Id,
                Permission = x.GetValue(null) as string,
            }));
    }
}
