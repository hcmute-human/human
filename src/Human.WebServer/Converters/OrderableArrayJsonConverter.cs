using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints;
using Human.Core.Constants;
using Human.Core.Models;

namespace Human.WebServer.Converters;

public class OrderableArrayJsonConverter : JsonConverter<Orderable[]>
{
    public override Orderable[] Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return reader.GetString()?.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x) && x.Length > 1).Select(x =>
                {
                    if (x![0] == '-')
                    {
                        return new Orderable
                        {
                            Name = x[1..].Trim(),
                            Order = Core.Constants.Order.Descending
                        };
                    }
                    return new Orderable
                    {
                        Name = x.Trim(),
                        Order = Core.Constants.Order.Ascending
                    };
                }).ToArray() ?? Array.Empty<Orderable>();
    }

    public override void Write(
        Utf8JsonWriter writer,
        Orderable[] value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(string.Join(',', value.Select(x => x.Order == Core.Constants.Order.Ascending ? x.Name : '-' + x.Name)));
    }

    public static ParseResult ValueParser(object? x)
    {
        return new ParseResult(true, x?.ToString()?.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x) && x.Length > 1).Select(x =>
                {
                    if (x![0] == '-')
                    {
                        return new Orderable
                        {
                            Name = x[1..].Trim(),
                            Order = Core.Constants.Order.Descending
                        };
                    }
                    return new Orderable
                    {
                        Name = x.Trim(),
                        Order = Core.Constants.Order.Ascending
                    };
                }).ToArray() ?? Array.Empty<Orderable>());
    }
}
