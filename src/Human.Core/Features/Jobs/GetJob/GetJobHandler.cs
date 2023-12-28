using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Jobs.GetJob;

public sealed class GetJobHandler(IAppDbContext dbContext) : ICommandHandler<GetJobCommand, Result<Job>>
{
    private readonly IAppDbContext dbContext = dbContext;

    public async Task<Result<Job>> ExecuteAsync(GetJobCommand command, CancellationToken ct)
    {
        var job = await dbContext.Jobs.FirstOrDefaultAsync(x => x.Id == command.Id, ct).ConfigureAwait(false);
        if (job is null)
        {
            return Result.Fail("Job does not exist")
              .WithName(nameof(command.Id))
              .WithCode("not_found")
              .WithStatus(HttpStatusCode.NotFound);
        }
        return job;
    }
}
