﻿using System;
using Quartz.Logging;

namespace TelegramBot.WebApi.Utils
{
    public class ConsoleLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info)
                {
                    Console.WriteLine($"[{DateTime.Now}] [{level}] {func?.Invoke()} {exception}", parameters);
                }
                return true;
            };
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}