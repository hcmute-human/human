using System.Dynamic;
using FastEndpoints;
using FluentValidation;
using Human.Core.Features.Emails.SendEmail;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Emails.SendEmail;

internal sealed class SendEmailRequest
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

internal sealed class Validator : Validator<SendEmailRequest>
{
    public Validator()
    {
        RuleFor(x => x.Subject)
            .NotEmpty();
        RuleFor(x => x.TemplateKey)
            .NotEmpty();
        RuleFor(x => x.Recipients)
            .NotEmpty();
        RuleForEach(x => x.Recipients)
            .ChildRules(x =>
            {
                x.RuleFor(x => x.Name).NotEmpty();
                x.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            });
    }
}

[Mapper]
internal static partial class RequestMapper
{
    public static partial SendEmailCommand ToCommand(this SendEmailRequest request);
}
