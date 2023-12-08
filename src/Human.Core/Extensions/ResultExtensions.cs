using System.Net;

namespace FluentResults;

public static class ResultExtensions
{
    public static Result WithName(this Result result, string name)
    {
        result.Errors.LastOrDefault()?.Metadata.Add("name", ToCamelCase(name));
        return result;
    }

    public static Result WithCode(this Result result, string code)
    {
        result.Errors.LastOrDefault()?.Metadata.Add("code", code.ToLowerInvariant());
        return result;
    }

    public static Result WithStatus(this Result result, HttpStatusCode statusCode)
    {
        result.Errors.LastOrDefault()?.Metadata.Add("statusCode", statusCode);
        return result;
    }

    public static string ToCamelCase(string text) => string.Concat(char.ToLowerInvariant(text[0]).ToString(), text.AsSpan(1));
}

public static class ErrorExtensions
{
    public static IError WithName(this IError error, string name)
    {
        error.Metadata.Add("name", name.ToLowerInvariant());
        return error;
    }

    public static IError WithCode(this IError error, string code)
    {
        error.Metadata.Add("code", code.ToLowerInvariant());
        return error;
    }

    public static IError WithStatus(this IError error, HttpStatusCode statusCode)
    {
        error.Metadata.Add("statusCode", statusCode);
        return error;
    }
}
