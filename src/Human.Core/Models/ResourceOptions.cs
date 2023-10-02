namespace Human.Core.Models;

public sealed class ResourceOptions
{
    public const string Section = "Resource";

    public required EmailTemplateResourceOptions EmailTemplate { get; set; }

    public sealed class EmailTemplateResourceOptions
    {
        public required string AssemblyName { get; set; }
        public required string RootNamespace { get; set; }
    }
}

