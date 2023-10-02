namespace Human.Core.Models;

public sealed class ApiOptions
{
    public const string Section = "Api";

    public required string RoutePrefix { get; set; }
}
