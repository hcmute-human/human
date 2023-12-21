using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Users.GetUser;

public sealed class GetUserHandler(IAppDbContext dbContext) : ICommandHandler<GetUserCommand, Result<User>>
{
    public async Task<Result<User>> ExecuteAsync(GetUserCommand command, CancellationToken ct)
    {
        var user = await dbContext.Users.Where(x => x.Id == command.Id).FirstOrDefaultAsync(ct).ConfigureAwait(false);
        if (user is null)
        {
            return Result.Fail("User does not exist")
              .WithName(nameof(command.Id))
              .WithCode("invalid_id")
              .WithStatus(HttpStatusCode.NotFound);
        }
        return user;
    }
}
