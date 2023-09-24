using System.Net;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;

namespace FastEndpoints;

public static class EndpointExtensions
{
    public static ProblemDetails ProblemDetails(this IEndpoint endpoint, IEnumerable<IError> errors)
    {
        var arr = errors as IError[] ?? errors.ToArray();
        var statusCode = arr[0].Metadata["statusCode"];
        return endpoint.ProblemDetails(arr, statusCode is HttpStatusCode code ? (int)code : 400);
    }

    public static ProblemDetails ProblemDetails(this IEndpoint endpoint, IEnumerable<IError> errors, int statusCode)
    {
        return new ProblemDetails(
            errors.Select(x => new ValidationFailure(x.Metadata["name"] as string ?? string.Empty, x.Message)
            {
                Severity = Severity.Error,
                ErrorCode = x.Metadata["code"] as string ?? string.Empty,
            }).ToList(),
            endpoint.Definition.Routes?[0] ?? string.Empty, endpoint.HttpContext.TraceIdentifier, statusCode);
    }
}
