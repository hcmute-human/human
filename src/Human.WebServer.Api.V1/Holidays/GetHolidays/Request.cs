using Human.Core.Features.Holidays.GetHolidays;
using Human.Core.Models;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Holidays.GetHolidays;

internal sealed class Request : Collective
{
    public string? Name { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetHolidaysCommand ToCommand(this Request request);
}
