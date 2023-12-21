using FastEndpoints;
using Human.Core.Constants;
using Human.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.Users.RequestAvatarUpload;

using Results = Results<Ok<Response>, ForbidHttpResult, ProblemDetails>;

internal sealed class Endpoint(IAuthorizationService authorizationService) : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Post("users/{Id}/avatars/upload-request");
        Verbs(Http.POST);
        Version(1);
        Description(x => x.Accepts<Request>());
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, new User { Id = request.Id }, AppPolicies.Users.Update).ConfigureAwait(false);
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
