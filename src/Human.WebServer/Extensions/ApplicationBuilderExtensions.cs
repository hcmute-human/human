using FastEndpoints;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Microsoft.AspNetCore.Builder;

internal interface IExceptionHandler { }

public static partial class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseProblemDetailsExceptionHandler(this IApplicationBuilder app, ILogger? logger = null, bool logStructuredException = false)
    {
        app.UseExceptionHandler(errApp =>
        {
            errApp.Run(async ctx =>
            {
                var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                if (exHandlerFeature is not null)
                {
                    Console.WriteLine(exHandlerFeature.Error);
                    logger ??= ctx.Resolve<ILogger<IExceptionHandler>>();
                    var http = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
                    var type = exHandlerFeature.Error.GetType().Name;
                    var error = exHandlerFeature.Error.Message;
                    var msg =
$@"=================================
{http}
TYPE: {type}
REASON: {error}
---------------------------------
{exHandlerFeature.Error.StackTrace}";

                    if (logStructuredException)
                        LogStructuredException(logger, http, type, error, exHandlerFeature.Error);
                    else
                        LogException(logger, msg);

                    ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    ctx.Response.ContentType = "application/problem+json";
                    await TypedResults.Problem(detail: error, statusCode: ctx.Response.StatusCode).ExecuteAsync(ctx).ConfigureAwait(false);
                }
            });
        });

        return app;
    }

    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "{http}{type}{reason}")]
    private static partial void LogStructuredException(ILogger logger, string? http, string? type, string? reason, Exception exception);
    [LoggerMessage(EventId = 1, Level = LogLevel.Error, Message = "{message}")]
    private static partial void LogException(ILogger logger, string message);
}
