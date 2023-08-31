using Serilog.Core;
using Serilog.Events;

namespace BasicApp.Modules.Logging;


public class LoggingEventHandler : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
        if (logEvent.Level == LogEventLevel.Warning || logEvent.Level == LogEventLevel.Error)
        {
            var notifyUserProperty = logEvent.Properties["NotifyUser"];
            if (notifyUserProperty != null && notifyUserProperty.ToString() == "True")
            {
                // Perform your notification process here
                HandleNotifyUser(logEvent);
            }
        }
    }

    private void HandleNotifyUser(LogEvent logEvent)
    {

    }
}
