using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;

namespace Human.Core.Features.Tests.CreateTest;

public sealed class CreateTestHandler(IAppDbContext dbContext) : ICommandHandler<CreateTestCommand, Result<Test>>
{
    public async Task<Result<Test>> ExecuteAsync(CreateTestCommand command, CancellationToken ct)
    {
        var test = command.ToTest();
        dbContext.Add(test);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return test;
    }
}
