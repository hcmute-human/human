namespace Human.Core.Interfaces;

public interface IEmailTemplateRenderer
{
    Task<string> RenderAsync<T>(string templateKey, T model, CancellationToken ct = default);
}
