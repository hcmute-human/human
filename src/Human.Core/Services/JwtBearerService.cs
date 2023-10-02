using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;
using Human.Core.Models;
using Human.Core.Interfaces;

namespace Human.Core.Services;

public class JwtBearerService : IJwtBearerService
{
    private readonly BearerOptions options;

    public JwtBearerService(IOptions<BearerOptions> options)
    {
        this.options = options.Value;
    }

    public string Sign(Action<UserPrivileges> privileges, DateTime? expireAt = null)
    {
        return Sign(options.ValidAudiences[0], privileges, expireAt);
    }

    public string Sign(string audience, Action<UserPrivileges> privileges, DateTime? expireAt = null)
    {
        return JWTBearer.CreateToken(signingKey: options.Secret, privileges: privileges,
issuer: options.ValidIssuer, audience: audience, expireAt: expireAt);
    }
}
