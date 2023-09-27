using MimeKit;

namespace Human.Infrastructure.Services;

public interface ISmtpClient
{
    Task SendAsync(Action<MimeMessage> configure, CancellationToken ct = default);
}
