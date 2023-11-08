using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Human.Core.Models;

namespace Human.Infrastructure.Persistence;

public class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
            .Build();

        var options = configuration.GetSection(PersistenceOptions.Section).Get<PersistenceOptions>()!;

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder
            .UseNpgsql(options.ConnectionString, x => x.UseNodaTime().MigrationsAssembly(options.MigrationsAssembly))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        return new AppDbContext(optionsBuilder.Options);
    }
}
