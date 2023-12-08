using Human.Core.Features.UserPermissions.GetUserPermissions;
using Human.Core.Models;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.UserPermissions.GetUserPermissions;

internal sealed class Response : PaginatedList<string> { }

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetUserPermissionsResult result);
    public static string MapUserPermission(UserPermission userPermission) => userPermission.Permission;
    // public static ICollection<string> MapUserPermission(ICollection<UserPermission> userPermissions) => userPermissions.Select(x => x.Permission).ToArray();
}
