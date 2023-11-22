using FastEndpoints;
using Human.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.DepartmentPositions.DeleteDepartmentPosition;

using Results = Results<Ok, ProblemDetails>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Permissions(Permit.DeleteDepartmentPosition);
        Delete("department-positions/{Id}");
        Verbs(Http.DELETE);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        var result = await request.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }
        return TypedResults.Ok();
    }
}
