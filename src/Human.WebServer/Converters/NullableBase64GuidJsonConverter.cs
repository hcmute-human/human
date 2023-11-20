using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints;
using Microsoft.AspNetCore.WebUtilities;

namespace Human.WebServer.Converters;

public class NullableBase64GuidJsonConverter : JsonConverter<Guid?>
{
    private readonly Base64GuidJsonConverter strictConverter = new();

    public override Guid? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var value = reader.GetString()!;
        if (value is null)
        {
            return default;
        }
        return strictConverter.Read(ref reader, typeToConvert, options);
    }

    public override void Write(
        Utf8JsonWriter writer,
        Guid? value,
        JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }
        strictConverter.Write(writer, value.Value, options);
    }

    public static ParseResult ValueParser(object? x)
    {
        if (x is null)
        {
            return new ParseResult(true, default);
        }
        return Base64GuidJsonConverter.ValueParser(x);
    }
}
