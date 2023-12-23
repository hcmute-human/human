using Human.Core.Features.Holidays.DeleteHoliday;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Holidays.DeleteHoliday;

internal sealed class Request
{
    public required Guid Id { get; set; }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial DeleteHolidayCommand ToCommand(this Request request);
}
