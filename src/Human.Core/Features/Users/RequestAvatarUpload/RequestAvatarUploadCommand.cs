using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Users.RequestAvatarUpload;

public sealed class RequestAvatarUploadCommand : ICommand<Result<RequestAvatarUploadResult>>
{
    public required Guid Id { get; set; }
}
