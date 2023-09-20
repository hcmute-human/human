using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using Human.Core.Models;

namespace Human.WebServer;

public static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Configure(builder.Services, builder.Configuration);

        var app = builder.Build();

        app.UseProblemDetailsExceptionHandler();
        app.UseStatusCodePages();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseFastEndpoints(x => x.Errors.UseProblemDetails());

        if (app.Environment.IsDevelopment())
        {
            app.MapGrpcReflectionService();
        }

        app.Run();
    }

    private static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddProblemDetails();
        services.AddFastEndpoints(x =>
        {
            x.DisableAutoDiscovery = true;
            x.Assemblies = new[]
            {
                Assembly.Load("Human.WebServer.Api.V1")
            };
        });
        var bearerSection = configuration.GetRequiredSection(BearerOptions.Section);
        var bearerOptions = bearerSection.Get<BearerOptions>()!;
        services
            .AddOptions<BearerOptions>()
            .Bind(bearerSection)
            .ValidateOnStart()
            .ValidateDataAnnotations();
        services.AddJWTBearerAuth(bearerOptions.Secret,
            tokenValidation: (options) =>
            {
                options.ValidateIssuerSigningKey = true;
                options.ValidateIssuer = true;
                options.ValidateAudience = true;
                options.ValidateLifetime = true;
                options.ValidateActor = false;
                options.ValidIssuer = bearerOptions.ValidIssuer;
                options.ValidAudience = null;
                options.ValidAudiences = bearerOptions.ValidAudiences;
            });
        services.AddAuthorization();

        var persistenceSection = configuration.GetRequiredSection(PersistenceOptions.Section);
        services.AddOptions<PersistenceOptions>()
            .Bind(persistenceSection)
            .ValidateOnStart()
            .ValidateDataAnnotations();

        services.AddCore();
        services.AddPersistence(persistenceSection.Get<PersistenceOptions>()!);
    }
}