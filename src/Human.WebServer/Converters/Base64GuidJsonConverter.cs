using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints;
using Microsoft.AspNetCore.WebUtilities;

namespace Human.WebServer.Converters;

public class Base64GuidJsonConverter : JsonConverter<Guid>
{
    public override Guid Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) => new(Base64UrlTextEncoder.Decode(reader.GetString()!));

    public override void Write(
        Utf8JsonWriter writer,
        Guid value,
        JsonSerializerOptions options) => writer.WriteStringValue(Base64UrlTextEncoder.Encode(value.ToByteArray()));

    public static ParseResult ValueParser(object? x)
    {
        byte[] bytes;
        try
        {
            bytes = Base64UrlTextEncoder.Decode(x?.ToString()!);
        }
        catch
        {
            return new ParseResult(false, Guid.Empty);
        }
        return bytes.Length == 16
            ? new ParseResult(true, new Guid(bytes))
            : new ParseResult(false, Guid.Empty);
    }
}
