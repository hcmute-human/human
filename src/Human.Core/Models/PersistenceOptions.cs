namespace Human.Core.Models;

public sealed class PersistenceOptions
{
    public const string Section = "Persistence";

    public required string ConnectionString { get; set; }
    public required string MigrationsAssembly { get; set; }
}
