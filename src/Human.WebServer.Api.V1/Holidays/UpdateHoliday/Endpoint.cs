using FastEndpoints;
using Human.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;
using NodaTime;

namespace Human.WebServer.Api.V1.Holidays.UpdateHoliday;

using Results = Results<NoContent, ProblemDetails>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Permissions(Permit.UpdateHoliday);
        Put("holidays/{Id}");
        Verbs(Http.PUT);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        request.EndTime ??= request.StartTime.InUtc().Date.PlusDays(1).AtStartOfDayInZone(DateTimeZone.Utc).Minus(Duration.FromSeconds(1)).ToInstant();
        var result = await request.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }
        return TypedResults.NoContent();
    }
}
