using System.Net;

namespace FluentResults;

public static class ResultExtensions
{
    public static Result WithName(this Result result, string name)
    {
        result.Errors.LastOrDefault()?.Metadata.Add("name", name.ToLowerInvariant());
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
}
