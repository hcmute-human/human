using MailKit.Net.Smtp;
using Human.Infrastructure.Models;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;

namespace Human.Infrastructure.Services;

public class GmailSmtpClient : ISmtpClient
{
    private readonly SmtpOptions options;

    public GmailSmtpClient(IOptions<SmtpOptions> options)
    {
        this.options = options.Value;
    }

    public async Task SendAsync(Action<MimeMessage> configure, CancellationToken ct = default)
    {
        var task = ConnectAsync(ct).ConfigureAwait(false);
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(options.SenderName, options.SenderAddress));
        configure(message);
        using var client = await task;
        await client.SendAsync(message, ct).ConfigureAwait(false);
        await client.DisconnectAsync(true, ct).ConfigureAwait(false);
    }

    private async Task<SmtpClient> ConnectAsync(CancellationToken ct = default)
    {
        var client = new SmtpClient();
        await client.ConnectAsync(
            options.Host,
            options.Port,
            options.Port == 465 ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls,
            ct).ConfigureAwait(false);
        await client.AuthenticateAsync(options.Username, options.Password, ct).ConfigureAwait(false);
        return client;
    }
}
