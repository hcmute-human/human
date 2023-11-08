using FastEndpoints;
using Human.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.Departments.GetDepartment;

using Results = Results<Ok<Response>, ProblemDetails>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Permissions(Permit.ReadDepartment);
        Get("departments/{Id}");
        Verbs(Http.GET);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        var result = await request.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }
        return TypedResults.Ok(result.Value.ToResponse());
    }
}
