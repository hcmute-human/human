using FastEndpoints;

namespace Human.Core.Interfaces;

public interface IJwtBearerService
{
    string Sign(string audience, Action<UserPrivileges> privileges, DateTime? expireAt = null);
    string Sign(Action<UserPrivileges> privileges, DateTime? expireAt = null);
}
