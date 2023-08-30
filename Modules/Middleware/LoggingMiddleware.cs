namespace Modules.Middleware;

using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Modules.Logging;

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

        Log.Data("Request Ip={Ip} Url={Url} Time={Time:000.000}", requesterIp.ToString() ?? "::::", requestUrl, timeTaken);
    }
}