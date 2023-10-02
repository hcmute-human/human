using System.Dynamic;
using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Emails.SendEmail;

public class SendEmailCommand : ICommand<Result<SendEmailResult>>
{
    public required string Subject { get; set; }
    public required string TemplateKey { get; set; }
    public required ExpandoObject TemplateModel { get; set; }
    public required ICollection<Recipient> Recipients { get; set; }

    public sealed class Recipient
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
    }
}
