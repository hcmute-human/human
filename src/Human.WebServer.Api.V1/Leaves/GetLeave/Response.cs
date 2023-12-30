using Human.Core.Features.Leaves.GetLeave;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Leaves.GetLeave;

internal sealed class Response
{
    public float TotalHours { get; set; }
    public float UsedHours { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial Response ToResponse(this GetLeaveResult result);
}
