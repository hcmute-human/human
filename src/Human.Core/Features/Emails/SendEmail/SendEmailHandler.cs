using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using MimeKit;
using MimeKit.Text;

namespace Human.Core.Features.Emails.SendEmail;

public sealed class SendEmailHandler : ICommandHandler<SendEmailCommand, Result<SendEmailResult>>
{
    private readonly IEmailTemplateRenderer renderer;
    private readonly ISmtpService smtpService;

    public SendEmailHandler(IEmailTemplateRenderer renderer, ISmtpService smtpService)
    {
        this.renderer = renderer;
        this.smtpService = smtpService;
    }

    public async Task<Result<SendEmailResult>> ExecuteAsync(SendEmailCommand command, CancellationToken ct)
    {
        var message = await smtpService.SendAsync(async m =>
        {
            m.Subject = command.Subject;
            m.To.AddRange(command.Recipients.Select(x => new MailboxAddress(x.Name, x.Email)));
            m.Body = new TextPart(TextFormat.Html)
            {
                Text = await renderer.RenderAsync(command.TemplateKey, command.TemplateModel).ConfigureAwait(false)
            };
        }, ct).ConfigureAwait(false);
        return new SendEmailResult { MessageId = message.MessageId };
    }
}
