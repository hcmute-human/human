using FastEndpoints;
using Human.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.UserPermissions.CheckCurrentUserHasPermission;

using Results = Results<Ok, NotFound>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Permissions(Permit.ReadUserPermission);
        Head("user-permissions/{Permission}");
        Verbs(Http.HEAD);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        var result = await request.ToCommand().ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok();
    }
}
