using Riok.Mapperly.Abstractions;
using Human.Core.Features.Holidays.GetHoliday;

namespace Human.WebServer.Api.V1.Holidays.GetHoliday;

internal sealed class Request
{
    public required Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial GetHolidayCommand ToCommand(this Request request);
}
