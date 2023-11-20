using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Users.CreateUser;

public sealed class CreateUserHandler : ICommandHandler<CreateUserCommand, Result<User>>
{
    private readonly IAppDbContext dbContext;

    public CreateUserHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<User>> ExecuteAsync(CreateUserCommand command, CancellationToken ct)
    {
        var any = await dbContext.Users.AnyAsync(x => x.Email == command.Email, ct).ConfigureAwait(false);
        if (any)
        {
            return Result.Fail("Email already exists")
              .WithName(nameof(command.Email))
              .WithCode("duplicated_email")
              .WithStatus(HttpStatusCode.BadRequest);
        }

        command.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(command.Password);
        var user = command.ToUser();
        dbContext.Add(user);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return user;
    }
}
