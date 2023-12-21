using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Human.WebServer.Converters;

public static class JsonSerializerOptionsDependencyInjection
{
    public static IServiceProvider ServiceProvider { get; set; } = null!;
}
