using System.Net;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using Human.Core.Interfaces;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using SystemTextJsonPatch.Exceptions;

namespace Human.Core.Features.Users.PatchUser;

public sealed class PatchUserHandler(IAppDbContext dbContext, IValidator<User> validator) : ICommandHandler<PatchUserCommand, Result<User>>
{
    public async Task<Result<User>> ExecuteAsync(PatchUserCommand command, CancellationToken ct)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct)
            .ConfigureAwait(false);

        if (user is null)
        {
            return Result.Fail("User not found")
                .WithName(nameof(command.Id))
                .WithCode("not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }

        if (command.Patch.Operations.Exists(x => x.Path?.Contains("avatar", StringComparison.OrdinalIgnoreCase) ?? false))
        {
            user.Avatar ??= new AssetInfo();
        }
        try
        {
            command.Patch.ApplyTo(user);
        }
        catch (JsonPatchException e)
        {
            return Result.Fail(e.Message)
                .WithName(nameof(command.Patch))
                .WithCode("patch_failed")
                .WithStatus(HttpStatusCode.BadRequest);
        }

        var validation = await validator.ValidateAsync(user, ct).ConfigureAwait(false);
        if (!validation.IsValid)
        {
            return Result.Fail(validation.Errors.Select(x => new Error(x.ErrorMessage)
                .WithName(x.PropertyName)
                .WithCode(x.ErrorCode)
                .WithStatus(HttpStatusCode.BadRequest)));
        }

        user.UpdatedTime = SystemClock.Instance.GetCurrentInstant();
        dbContext.Update(user);
        await dbContext.SaveChangesAsync(ct).ConfigureAwait(false);
        return user;
    }
}
