using FastEndpoints;
using Human.Core.Constants;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.LeaveApplications.GetLeaveApplication;

using Results = Results<Ok<Response>, ProblemDetails, ForbidHttpResult>;

internal sealed class Endpoint(IAuthorizationService authorizationService) : Endpoint<Request, Results>
{
    private readonly IAuthorizationService authorizationService = authorizationService;
    public override void Configure()
    {
        Get("leave-applications/{Id}");
        Verbs(Http.GET);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        if (!request.HasReadPermission)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, new LeaveApplication { Id = request.Id }, AppPolicies.LeaveApplication.Read).ConfigureAwait(false);
            if (!authorizationResult.Succeeded)
            {
                return TypedResults.Forbid();
            }
        }

        var result = await request.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }
        return TypedResults.Ok(result.Value.ToResponse());
    }
}
