using FastEndpoints;
using Human.Core.Constants;
using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.EmployeePositions.GetEmployeePosition;

using Results = Results<Ok<Response>, ForbidHttpResult, ProblemDetails>;

internal sealed class Endpoint(IAuthorizationService authorizationService) : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Get("employees/{EmployeeId}/positions/{DepartmentPositionId}");
        Verbs(Http.GET);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, new EmployeePosition { EmployeeId = request.EmployeeId, DepartmentPositionId = request.DepartmentPositionId }, AppPolicies.EmployeePositions.Read).ConfigureAwait(false);
        if (!authResult.Succeeded)
        {
            return TypedResults.Forbid();
        }
        var result = await request.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }
        return TypedResults.Ok(result.Value.ToResponse());
    }
}
