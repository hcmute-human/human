using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.Auth.Authenticate;

internal sealed class Endpoint : EndpointWithoutRequest<NoContent>
{
    public override void Configure()
    {
        Post("auth/authenticate");
        Verbs(Http.POST);
        Version(1);
    }

    public override Task<NoContent> ExecuteAsync(CancellationToken ct)
    {
        return Task.FromResult(TypedResults.NoContent());
    }
}