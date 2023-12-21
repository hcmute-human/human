using System.Text.Json;
using System.Text.Json.Serialization;
using Human.Core.Interfaces;
using Human.Domain.Models;

namespace Human.WebServer.Converters;

public class AssetInfoJsonConverter : JsonConverter<AssetInfo?>
{
    private readonly JsonConverter<AssetInfo?> defaultConverter = (JsonConverter<AssetInfo?>)JsonSerializerOptions.Default.GetConverter(typeof(AssetInfo));

    public override bool HandleNull => true;

    public override AssetInfo? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return defaultConverter.Read(ref reader, typeToConvert, options);
    }

    public override void Write(
        Utf8JsonWriter writer,
        AssetInfo? value,
        JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }
        writer.WriteStringValue(JsonSerializerOptionsDependencyInjection.ServiceProvider.GetRequiredService<IAssetService>().GenerateUrl(value) ?? string.Empty);
    }

    // public static ParseResult ValueParser(object? x)
    // {
    //     var value = x?.ToString();
    //     if (value is null)
    //     {
    //         return new ParseResult(false, Guid.Empty);
    //     }

    //     if (Guid.TryParseExact(value, "D", out var guid))
    //     {
    //         return new ParseResult(true, guid);
    //     }

    //     byte[] bytes;
    //     try
    //     {
    //         bytes = Base64UrlTextEncoder.Decode(value);
    //     }
    //     catch
    //     {
    //         return new ParseResult(false, Guid.Empty);
    //     }
    //     return bytes.Length == 16
    //         ? new ParseResult(true, new Guid(bytes))
    //         : new ParseResult(false, Guid.Empty);
    // }
}
