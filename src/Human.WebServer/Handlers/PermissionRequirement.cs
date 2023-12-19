using Microsoft.AspNetCore.Authorization;

namespace Human.WebServer.Handlers;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; set; } = permission;
}
