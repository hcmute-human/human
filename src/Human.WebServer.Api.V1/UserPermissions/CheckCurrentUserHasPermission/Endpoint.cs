using FastEndpoints;
using Human.Core.Constants;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.UserPermissions.CheckCurrentUserHasPermission;

using Results = Results<Ok, ForbidHttpResult, NotFound>;

internal sealed class Endpoint(IAuthorizationService authorizationService) : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Head("user-permissions/{Permission}");
        Verbs(Http.HEAD);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, new UserPermission { UserId = request.UserId }, AppPolicies.UserPermissions.Read).ConfigureAwait(false);
        if (!authResult.Succeeded)
        {
            return TypedResults.Forbid();
        }

        var result = await request.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok();
    }
}
