using FastEndpoints;
using Human.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.EmployeePositions.CreateEmployeePosition;

using Results = Results<NoContent, ProblemDetails>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Permissions(Permit.CreateEmployeePosition);
        Post("employees/{EmployeeId}/positions");
        Verbs(Http.POST);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        var result = await request.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }
        return TypedResults.NoContent();
    }
}
