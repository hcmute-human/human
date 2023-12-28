using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;

namespace Human.Core.Features.Jobs.CreateJob;

public sealed class CreateJobHandler(IAppDbContext dbContext) : ICommandHandler<CreateJobCommand, Result<Job>>
{
    public async Task<Result<Job>> ExecuteAsync(CreateJobCommand command, CancellationToken ct)
    {
        var job = command.ToJob();
        dbContext.Add(job);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return job;
    }
}
