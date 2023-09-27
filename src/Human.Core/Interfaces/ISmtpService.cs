using MimeKit;

namespace Human.Core.Interfaces;

public interface ISmtpService
{
    Task SendAsync(Action<MimeMessage> configure, CancellationToken ct = default);
}
