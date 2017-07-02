using Microsoft.Extensions.Configuration;
using Serilog.Events;
using System;

namespace MyMassage.Backend
{
    public interface ISettings
    {
        LogEventLevel LogLevel { get; }
        string DatabaseUrl { get; }
        string DatabaseName { get; }
    }

    public class Settings : ISettings
    {
        public Settings(IConfiguration config)
        {
            LogLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), config["Logging:LogLevel"]);
            DatabaseUrl = config["Database:Url"];
            DatabaseName = config["Database:Name"];
        }

        public LogEventLevel LogLevel { get; private set; }

        public string DatabaseUrl { get; private set; }

        public string DatabaseName { get; private set; }
    }
}
