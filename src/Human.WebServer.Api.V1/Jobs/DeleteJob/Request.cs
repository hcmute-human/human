using Human.Core.Features.Jobs.DeleteJob;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Jobs.DeleteJob;

internal sealed class Request
{
    public required Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteJobCommand ToCommand(this Request request);
}
