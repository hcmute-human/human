using FastEndpoints;

namespace Human.WebServer.Api.V1.Users.SignUp;

public sealed class Endpoint : EndpointWithoutRequest<object>
{
    public Endpoint() { }

    public override void Configure()
    {
        Get(string.Empty);
        Verbs(Http.GET);
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(string.Empty);
    }
}
