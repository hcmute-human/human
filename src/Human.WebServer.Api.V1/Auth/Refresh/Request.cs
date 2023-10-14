using Human.Core.Features.Auth.Refresh;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Auth.Refresh;

internal sealed class Request
{
    public required Guid RefreshToken { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial RefreshCommand ToCommand(this Request request);
}
