using Human.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Human.Core.Models;
using Human.Core.Interfaces;

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
                .EnableDetailedErrors()
                .UseModel(Human.Infrastructure.Persistence.CompiledModels.AppDbContextModel.Instance);
        });
        self.AddScoped<IAppDbContext>(x => x.GetRequiredService<AppDbContext>());
        return self;
    }
}
