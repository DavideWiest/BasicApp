using Serilog;
using System.Diagnostics;

namespace Modules.Middleware;

using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

public class LoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        await next(context);

        stopwatch.Stop();

        var requestUrl = context.Request.Path;
        var requesterIp = context.Connection.RemoteIpAddress;
        var timeTaken = stopwatch.Elapsed.TotalMilliseconds;

        Log.Information("Request from {Ip}: {Url} took {Time} ms", requesterIp, requestUrl, timeTaken);
    }
}