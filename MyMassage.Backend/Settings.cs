using Microsoft.Extensions.Configuration;
using Serilog.Events;
using System;

namespace MyMassage.Backend
{
    public interface ISettings
    {
        LogEventLevel LogLevel { get; }

        string Database_Url { get; }
        string Database_Name { get; }

        string SMTP_Address { get; }
        string SMTP_UserName { get; }
        string SMTP_Password { get; }
        string SMTP_Host { get; }
        int SMTP_Port { get; }
        bool SMTP_EnableSsl { get; }
    }

    public class Settings : ISettings
    {
        public Settings(IConfiguration config)
        {
            LogLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), config["Logging:LogLevel"]);

            Database_Url = config["Database:Url"];
            Database_Name = config["Database:Name"];

            SMTP_Address = config["SMTP:Address"];
            SMTP_UserName = config["SMTP:UserName"];
            SMTP_Password = config["SMTP:Password"];
            SMTP_Host = config["SMTP:Host"];
            SMTP_Port = Convert.ToInt32(config["SMTP:Port"]);
            SMTP_EnableSsl = Convert.ToBoolean(config["SMTP:EnableSsl"]);
        }

        public LogEventLevel LogLevel { get; private set; }

        public string Database_Url { get; private set; }
        public string Database_Name { get; private set; }

        public string SMTP_Address { get; private set; }
        public string SMTP_UserName { get; private set; }
        public string SMTP_Password { get; private set; }
        public string SMTP_Host { get; private set; }
        public int SMTP_Port { get; private set; }
        public bool SMTP_EnableSsl { get; private set; }
    }
}
