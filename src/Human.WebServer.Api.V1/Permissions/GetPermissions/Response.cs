using Human.Core.Features.Permissions.GetPermissions;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Permissions.GetPermissions;

internal sealed class Response : PaginatedList<string>
{
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetPermissionsResult result);
}
