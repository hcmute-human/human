using System.Net;
using System.Reflection;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Human.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;

namespace FastEndpoints;

public static class EndpointExtensions
{
    public static ProblemDetails ProblemDetails(this IEndpoint endpoint, IEnumerable<IError> errors)
    {
        var arr = errors as IError[] ?? errors.ToArray();
        var statusCode = arr[0].Metadata.GetValueOrDefault("statusCode");
        return endpoint.ProblemDetails(arr, statusCode is HttpStatusCode code ? (int)code : 400);
    }

    public static ProblemDetails ProblemDetails(this IEndpoint endpoint, IEnumerable<IError> errors, int statusCode)
    {
        return new ProblemDetails(
            errors.Select(x => new ValidationFailure(x.Metadata.GetValueOrDefault("name") as string ?? string.Empty, x.Message)
            {
                Severity = Severity.Error,
                ErrorCode = x.Metadata.GetValueOrDefault("code") as string ?? string.Empty,
            }).ToList(),
            endpoint.Definition.Routes?[0] ?? string.Empty, endpoint.HttpContext.TraceIdentifier, statusCode);
    }

    public static CreatedAtEndpoint<TEndpoint, TValue> CreatedAt<TEndpoint, TValue>(this IEndpoint self, object routeValues)
        where TEndpoint : IEndpoint
    {
        return new CreatedAtEndpoint<TEndpoint, TValue>(routeValues, default);
    }

    public static CreatedAtEndpoint<TEndpoint, TValue> CreatedAt<TEndpoint, TValue>(this IEndpoint self, TValue value)
        where TEndpoint : IEndpoint
    {
        return new CreatedAtEndpoint<TEndpoint, TValue>(default, value);
    }

    public static CreatedAtEndpoint<TEndpoint, TValue> CreatedAt<TEndpoint, TValue>(this IEndpoint self, object routeValues, TValue value)
        where TEndpoint : IEndpoint
    {
        return new CreatedAtEndpoint<TEndpoint, TValue>(routeValues, value);
    }

    public static Pageable Pageable(this IEndpoint endpoint)
    {
        if (!endpoint.HttpContext.Request.Query.TryGetValue("page", out var pageStr) || !int.TryParse(pageStr.FirstOrDefault(), out var page))
        {
            page = 1;
        }
        if (!endpoint.HttpContext.Request.Query.TryGetValue("size", out var sizeStr) || !int.TryParse(sizeStr.FirstOrDefault(), out var size))
        {
            size = 10;
        }
        return new Pageable(page, size);
    }

    public static ICollection<Orderable> Sortables(this IEndpoint endpoint)
    {
        var sortables = new List<Orderable>();
        if (!endpoint.HttpContext.Request.Query.TryGetValue("page", out var sorts))
        {
            foreach (var sort in sorts)
            {
                sortables.AddRange(sorts.Where(x => !string.IsNullOrEmpty(x)).Select(x =>
                {
                    if (x![0] == '-')
                    {
                        return new Orderable
                        {
                            Name = x[1..].Trim(),
                            Order = Human.Core.Constants.Order.Descending
                        };
                    }
                    return new Orderable
                    {
                        Name = x.Trim(),
                        Order = Human.Core.Constants.Order.Ascending
                    };
                }));
            }
        }
        return sortables;
    }
}

public sealed class CreatedAtEndpoint<TEndpoint, TValue> : IResult, IEndpointMetadataProvider, IStatusCodeHttpResult, IValueHttpResult, IValueHttpResult<TValue>
    where TEndpoint : IEndpoint
{
    private readonly object? routeValues;

    public CreatedAtEndpoint(object? routeValues, TValue? value)
    {
        this.routeValues = routeValues;
        Value = value;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        return httpContext.Response.SendCreatedAtAsync<TEndpoint>(routeValues, Value, generateAbsoluteUrl: true);
    }

    static void IEndpointMetadataProvider.PopulateMetadata(MethodInfo method, EndpointBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(method);
        ArgumentNullException.ThrowIfNull(builder);

        builder.Metadata.Add(new ProducesResponseTypeMetadata(typeof(TValue), StatusCodes.Status201Created, "application/json"));
    }

    public int StatusCode => StatusCodes.Status201Created;

    int? IStatusCodeHttpResult.StatusCode => StatusCode;
    object? IValueHttpResult.Value => Value;
    public TValue? Value { get; }
}

internal sealed class ProducesResponseTypeMetadata : IProducesResponseTypeMetadata
{
    public ProducesResponseTypeMetadata(Type type, int statusCode, string contentType)
    {
        Type = type;
        StatusCode = statusCode;
        ContentTypes = new[] { contentType };
    }

    public Type? Type { get; set; }

    public int StatusCode { get; set; }

    public IEnumerable<string> ContentTypes { get; set; }

    public object? Example { get; set; }
}
