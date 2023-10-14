using FastEndpoints;
using Human.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.Departments.CreateDepartment;

using Results = Results<CreatedAtEndpoint<GetDepartment.Endpoint, Response>, ProblemDetails>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Permissions(Permit.CreateDepartment);
        Post("departments");
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
        return this.CreatedAt<GetDepartment.Endpoint, Response>(new { result.Value.Id }, result.Value.ToResponse());
    }
}
