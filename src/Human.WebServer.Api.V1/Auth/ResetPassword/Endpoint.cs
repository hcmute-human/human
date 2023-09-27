using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.Auth.ResetPassword;

using Response = Results<Ok<ResetPasswordResponse>, ProblemDetails>;

internal sealed class Endpoint : Endpoint<ResetPasswordRequest, Response>
{
    public override void Configure()
    {
        Post("auth/reset-password");
        Verbs(Http.POST);
        Version(1);
        AllowAnonymous();
    }

    public override async Task<Response> ExecuteAsync(ResetPasswordRequest req, CancellationToken ct)
    {
        var result = await req.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }

        return TypedResults.Ok(result.Value.ToResponse());
    }
}
