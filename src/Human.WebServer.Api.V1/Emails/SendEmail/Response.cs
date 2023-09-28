using Human.Core.Features.Emails.SendEmail;
using Riok.Mapperly.Abstractions;

namespace Human.WebServer.Api.V1.Emails.SendEmail;

internal sealed class SendEmailResponse
{
    public required string MessageId { get; set; }
}

[Mapper]
internal static partial class ResponseMapper
{
    public static partial SendEmailResponse ToResponse(this SendEmailResult result);
}
