using FastEndpoints;
using Human.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.LeaveApplications.CountLeaveApplications;

using Results = Results<NoContent, ProblemDetails>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Permissions(Permit.ReadLeaveApplication);
        Head("leave-applications");
        Verbs(Http.HEAD);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        var result = await request.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }
        HttpContext.Response.Headers.ContentRange = $"items 0-0/{result.Value.TotalCount}";
        return TypedResults.NoContent();
    }
}
