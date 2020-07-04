using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace DiaryApp.API
{
    public class FileLogger : ILogger
    {
        private readonly string filePath;
        private static readonly object _lock = new object();
        public FileLogger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }

    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string path;
        public FileLoggerProvider(string _path)
        {
            path = _path;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(path);
        }

        public void Dispose()
        {
        }
    }
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,
                                        string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }

        public static void LogErrorWithDate(this ILogger logger, Exception ex)
        {
            logger.LogErrorWithDate($"{ex.Message} {Environment.NewLine} {ex.StackTrace}");
        }

        public static void LogErrorWithDate(this ILogger logger, string message)
        {
            logger.LogWarning($"{DateTime.UtcNow} {message}");
        }
    }
}
