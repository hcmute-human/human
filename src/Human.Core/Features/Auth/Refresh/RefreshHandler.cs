using System.Net;
using System.Security.Claims;
using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Human.Core.Features.Auth.Refresh;

public sealed class RefreshHandler : ICommandHandler<RefreshCommand, Result<RefreshResult>>
{
    private readonly IAppDbContext dbContext;
    private readonly IJwtBearerService jwtBearerService;

    public RefreshHandler(IAppDbContext dbContext, IJwtBearerService jwtBearerService)
    {
        this.dbContext = dbContext;
        this.jwtBearerService = jwtBearerService;
    }

    public async Task<Result<RefreshResult>> ExecuteAsync(RefreshCommand command, CancellationToken ct)
    {
        var refreshToken = await dbContext.UserRefreshTokens.Where(x => x.Token == command.RefreshToken)
            .Select(x => new { UserId = x.User.Id, x.ExpiryTime })
            .FirstOrDefaultAsync(ct)
            .ConfigureAwait(false);

        if (refreshToken is null)
        {
            return Result.Fail("Refresh token not found")
                .WithName(nameof(command.RefreshToken))
                .WithCode("refresh_token_not_found")
                .WithStatus(HttpStatusCode.NotFound);
        }

        if (SystemClock.Instance.GetCurrentInstant() > refreshToken.ExpiryTime)
        {
            return Result.Fail("Refresh token has been expired")
                .WithName(nameof(command.RefreshToken))
                .WithCode("refresh_token_expired")
                .WithStatus(HttpStatusCode.Forbidden);
        }

        return new RefreshResult
        {
            AccessToken = jwtBearerService.Sign(
                privileges => privileges.Claims.Add(new Claim(ClaimTypes.NameIdentifier, refreshToken.UserId.ToString())),
                expireAt: DateTimeOffset.UtcNow.AddMinutes(5).UtcDateTime),
            RefreshToken = command.RefreshToken.ToString()
        };
    }
}
