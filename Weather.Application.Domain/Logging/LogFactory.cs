

using System;
using Microsoft.Extensions.Logging;
using Weather.Application.Domain.Enums;

namespace Weather.Application.Domain.Logging
{
    public class LogFactory
    {

        public ILogger CreateLogger(LoggerType loggerType, LogLevel level, string category, string configuration = null) => loggerType switch
        {
            LoggerType.Console => CreateConsoleLogger(level,category),
            LoggerType.Database => CreateDatabaseLogger(level, category, configuration),
            LoggerType.File => CreateFileLogger(level, category, configuration),
            _ => throw new ArgumentException("no logger type selected")
        };


        #region Private members

        private ILogger CreateConsoleLogger(LogLevel level, string category)
        {
            var consoleLoggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .AddFilter("Weather", level)
                .AddConsole();
            });

            return consoleLoggerFactory.CreateLogger(category);
        }

        private ILogger CreateDatabaseLogger(LogLevel level, string category, string configuration)
        {
            // Todo : Implement database logger
            throw new NotImplementedException();
        }

        private ILogger CreateFileLogger(LogLevel level, string category, string configuration)
        {
            // Todo : Implement database logger
            throw new NotImplementedException();
        }

        #endregion Private Members
    }
}
