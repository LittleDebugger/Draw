using System;
using Draw.Console.IO.Interfaces;
using Microsoft.Extensions.Logging;

namespace Draw.Console.IO
{
    internal class ConsoleLogger : ILogger
    {
        private readonly IConsoleWriter _consoleWriter;

        public ConsoleLogger(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (logLevel != LogLevel.Debug)
            {
                _consoleWriter.WriteLine(formatter.Invoke(state, exception));
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}