using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Human.WebServer.Api.V1.LeaveApplications.PatchLeaveApplication;

using Results = Results<NoContent, ProblemDetails, ForbidHttpResult>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    public override void Configure()
    {
        Patch("leave-applications/{Id}");
        Verbs(Http.PATCH);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        var isForbidden = (request.CanProcess, request.CanUpdate) switch
        {
            (false, false) => true,
            (true, false) => request.Patch!.Operations.Exists(x => !x.Path?.Equals("/status", StringComparison.OrdinalIgnoreCase) ?? false),
            _ => false
        };
        if (isForbidden)
        {
            return TypedResults.Forbid();
        }

        var command = request.ToCommand();
        command.Patch.Replace(x => x.ProcessorId, request.ProcessorId);
        var result = await command.ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }
        return TypedResults.NoContent();
    }
}
