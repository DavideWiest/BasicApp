﻿namespace Modules.Logging;

using Serilog;
using Serilog.Events;

public static class Log
{
    private static readonly ILogger SLogger = new LoggerConfiguration()
    .Enrich.With(new LoggingOptionEnricher())
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning || evt.Level == LogEventLevel.Error || evt.Level == LogEventLevel.Fatal)
        .WriteTo.File("Logs/priority.txt", rollingInterval: RollingInterval.Day)
    )
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error || evt.Level == LogEventLevel.Fatal)
        .WriteTo.File("Logs/error.txt", rollingInterval: RollingInterval.Day)
    )
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Sink(new LoggingEventHandler())
    .CreateLogger();

    public static void Verbose(string messageTemplate, params object[] propertyValues)
    {
        SLogger.Verbose(messageTemplate, propertyValues);
    }

    public static void Debug(string messageTemplate, params object[] propertyValues)
    {
        SLogger.Debug(messageTemplate, propertyValues);
    }

    public static void Information(string messageTemplate, params object[] propertyValues)
    {
        SLogger.Information(messageTemplate, propertyValues);
    }

    public static void Warning(string messageTemplate, params object[] propertyValues)
    {
        SLogger.Warning(messageTemplate, propertyValues);
    }

    public static void Error(string messageTemplate, params object[] propertyValues)
    {
        SLogger.Error(messageTemplate, propertyValues);
    }

    public static void Fatal(string messageTemplate, params object[] propertyValues)
    {
        SLogger.Fatal(messageTemplate, propertyValues);
    }
}
