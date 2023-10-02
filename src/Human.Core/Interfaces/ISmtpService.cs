using MimeKit;

namespace Human.Core.Interfaces;

public interface ISmtpService
{
    Task<MimeMessage> SendAsync(Action<MimeMessage> configure, CancellationToken ct = default);
}
