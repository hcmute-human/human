using Human.Core.Models;

namespace Human.Web;

public static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Configure(builder.Services, builder.Configuration);

        var app = builder.Build();
        app.Run();
    }

    private static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        var persistenceSection = configuration.GetRequiredSection(PersistenceOptions.Section);
        services.AddOptions<PersistenceOptions>()
            .Bind(persistenceSection)
            .ValidateOnStart()
            .ValidateDataAnnotations();

        services.AddPersistence(persistenceSection.Get<PersistenceOptions>()!);
    }
}
