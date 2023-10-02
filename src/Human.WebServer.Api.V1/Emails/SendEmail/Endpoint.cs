using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.Emails.SendEmail;

using Response = Results<Ok<SendEmailResponse>, ProblemDetails>;

internal sealed class Endpoint : Endpoint<SendEmailRequest, Response>
{
    public override void Configure()
    {
        Post("emails/send");
        Verbs(Http.POST);
        Version(1);
    }

    public override async Task<Response> ExecuteAsync(SendEmailRequest req, CancellationToken ct)
    {
        var result = await req.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }

        return TypedResults.Ok(result.Value.ToResponse());
    }
}
