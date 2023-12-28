using Riok.Mapperly.Abstractions;
using Human.Core.Features.Jobs.GetJob;

namespace Human.WebServer.Api.V1.Jobs.GetJob;

internal sealed class Request
{
    public required Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetJobCommand ToCommand(this Request request);
}
