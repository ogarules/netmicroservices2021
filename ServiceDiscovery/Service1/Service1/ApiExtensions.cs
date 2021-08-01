using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Service1
{
    public static class ApiExtensions
    {
        public static void OpenLogInformation(this ILogger logger, string message)
        {
            logger.LogInformation(message);
            Activity.Current?.AddEvent(new ActivityEvent(message));
        }

        public static void OpenLogError(this ILogger logger, string message, Exception exception)
        {
            logger.LogError(exception, message);
            Activity.Current?.AddEvent(new ActivityEvent($"Exception => {message} : {exception.ToString()}"));
        }
    }
}