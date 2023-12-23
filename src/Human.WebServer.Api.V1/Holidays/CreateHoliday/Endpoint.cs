using FastEndpoints;
using Human.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;
using NodaTime;
using NodaTime.Extensions;

namespace Human.WebServer.Api.V1.Holidays.CreateHoliday;

using Results = Results<Ok<Response>, ProblemDetails>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Permissions(Permit.CreateHoliday);
        Post("holidays");
        Verbs(Http.POST);
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
        return TypedResults.Ok(result.Value.ToResponse());
    }
}
