using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.Auth.CreatePasswordResetToken;

using Response = Results<Ok<CreatePasswordResetTokenResponse>, ProblemDetails>;

internal sealed class Endpoint : Endpoint<CreatePasswordResetTokenRequest, Response>
{
    public override void Configure()
    {
        Post("auth/reset-password");
        Verbs(Http.POST);
        Version(1);
        AllowAnonymous();
    }

    public override async Task<Response> ExecuteAsync(CreatePasswordResetTokenRequest req, CancellationToken ct)
    {
        var result = await req.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }

        return TypedResults.Ok(result.Value.ToResponse());
    }
}
