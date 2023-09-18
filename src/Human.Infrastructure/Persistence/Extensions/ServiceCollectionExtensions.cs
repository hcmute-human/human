using Human.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Human.Core.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection self, PersistenceOptions options)
    {
        self.AddDbContextPool<AppDbContext>(x =>
        {
            x.UseNpgsql(options.ConnectionString, x => x.UseNodaTime().MigrationsAssembly(options.MigrationsAssembly))
#if DEBUG
                .EnableSensitiveDataLogging()
#endif
                .EnableDetailedErrors();
        });
        return self;
    }
}
