using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Jobs.DeleteJob;

public sealed class DeleteJobHandler(IAppDbContext dbContext) : ICommandHandler<DeleteJobCommand, Result>
{
    public async Task<Result> ExecuteAsync(DeleteJobCommand command, CancellationToken ct)
    {
        var count = await dbContext.Jobs
            .Where(x => x.Id == command.Id)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
        if (count == 0)
        {
            return Result.Fail("Job not found")
                .WithName(nameof(command.Id))
                .WithCode("not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }
        return Result.Ok();
    }
}
