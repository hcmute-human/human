using Riok.Mapperly.Abstractions;
using Human.Core.Features.Users.GetUser;

namespace Human.WebServer.Api.V1.Users.GetUser;

internal sealed class Request
{
    public required Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetUserCommand ToCommand(this Request request);
}
