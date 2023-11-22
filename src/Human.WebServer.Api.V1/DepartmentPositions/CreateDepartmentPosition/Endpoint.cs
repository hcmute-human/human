using FastEndpoints;
using Human.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.DepartmentPositions.CreateDepartmentPosition;

using Results = Results<CreatedAtEndpoint<Departments.GetDepartment.Endpoint, Response>, ProblemDetails>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Permissions(Permit.CreateDepartmentPosition);
        Post("departments/{DepartmentId}/positions");
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
        return this.CreatedAt<Departments.GetDepartment.Endpoint, Response>(new { result.Value.Id }, result.Value.ToResponse());
    }
}
