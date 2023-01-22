using Microsoft.Extensions.Logging;

namespace AzureDatabaseTools.Helpers;

public static class LoggingHelper
{
    public static string? GetMinimumLevelFromArguments(string[] commandLineArgs)
    {
        string? verbosity = null;
        int index = 0;

        foreach (string arg in commandLineArgs)
        {
            if (arg is "--verbosity" or "-v")
            {
                return commandLineArgs[index + 1];
            }

            index++;
        }

        return verbosity;
    }

    public static LogLevel ConvertStringToLogLevel(this string? logLevel)
    {
        return logLevel switch
        {
            "Trace" => LogLevel.Trace,
            "Debug" => LogLevel.Debug,
            "Warning" => LogLevel.Warning,
            "Error" => LogLevel.Error,
            "Critical" => LogLevel.Critical,
            "Information" or _ => LogLevel.Information
        };
    }
}