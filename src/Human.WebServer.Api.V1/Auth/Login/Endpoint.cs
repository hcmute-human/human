using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.Auth.Login;

using Response = Results<Ok<LoginResponse>, ProblemDetails>;

internal sealed class Endpoint : Endpoint<LoginRequest, Response>
{
    public override void Configure()
    {
        Post("auth/login");
        Verbs(Http.POST);
        Version(1);
        AllowAnonymous();
    }

    public override async Task<Response> ExecuteAsync(LoginRequest req, CancellationToken ct)
    {
        var result = await req.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }

        return TypedResults.Ok(result.Value.ToResponse());
    }
}