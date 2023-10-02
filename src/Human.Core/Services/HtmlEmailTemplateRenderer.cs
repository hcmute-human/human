using Human.Core.Interfaces;
using RazorLight;
using RazorLight.Extensions;

namespace Human.Core.Services;

public class HtmlEmailTemplateRenderer : IEmailTemplateRenderer
{
    private readonly IRazorLightEngine engine;

    public HtmlEmailTemplateRenderer(IRazorLightEngine engine)
    {
        this.engine = engine;
    }

    public Task<string> RenderAsync<T>(string templateKey, T model, CancellationToken ct = default)
    {
        return engine.CompileRenderAsync(templateKey, model.ToExpando());
    }
}
