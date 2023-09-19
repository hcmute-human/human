using Human.Core.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection self)
    {
        self.AddSingleton<IJwtBearerService, JwtBearerService>();
        return self;
    }
}
