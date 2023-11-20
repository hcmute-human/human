using Human.Core.Interfaces;
using Human.Core.Models;
using Human.Infrastructure.Persistence;
using Human.Infrastructure.Services;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection self)
    {
        self.AddDbContextPool<AppDbContext>((provider, builder) =>
        {
            var options = provider.GetRequiredService<IOptions<PersistenceOptions>>().Value;
            builder
                .UseNpgsql(
                    options.ConnectionString,
                    builder => builder.UseNodaTime().MigrationsAssembly(options.MigrationsAssembly))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
#if DEBUG
                .EnableSensitiveDataLogging()
#endif
                .EnableDetailedErrors()
                .UseModel(Human.Infrastructure.Persistence.CompiledModels.AppDbContextModel.Instance);
        });
        self.AddScoped<IAppDbContext>(x => x.GetRequiredService<AppDbContext>());
        self.AddSingleton<ISmtpService, GmailSmtpService>();
        return self;
    }
}
