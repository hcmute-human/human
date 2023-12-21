using System.Net;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Users.RequestAvatarUpload;

public sealed class RequestAvatarUploadHandler(IAppDbContext dbContext, IAssetService assetService) : ICommandHandler<RequestAvatarUploadCommand, Result<RequestAvatarUploadResult>>
{
    public async Task<Result<RequestAvatarUploadResult>> ExecuteAsync(RequestAvatarUploadCommand command, CancellationToken ct)
    {
        var any = await dbContext.Users.AnyAsync(x => x.Id == command.Id, ct).ConfigureAwait(false);
        if (!any)
        {
            return Result.Fail("User not found")
              .WithName(nameof(command.Id))
              .WithCode("not_found")
              .WithStatus(HttpStatusCode.NotFound);
        }
        return new RequestAvatarUploadResult
        {
            UploadUrl = assetService.SignImageUploadUrl(
                new
                {
                    public_id = "avatar",
                    folder = command.Id.ToString(),
                }),
        };
    }
}
