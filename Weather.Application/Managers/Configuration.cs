using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Weather.Application.ConsoleApp.Managers
{
    public class Configuration
    {
        private IConfiguration _configuration;
        private IConfiguration Config
        {
            get
            {
                if (_configuration == null)
                    _configuration = BuildConfig();
                return _configuration;
            }
        }

        private IConfiguration BuildConfig()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true,
                                reloadOnChange: true);
            return builder.Build();
        }

        public string ApiUrl => Config.GetSection("apiSettings").GetValue<string>("baseUrl");

        public string ApiKey => Config.GetSection("apiSettings").GetValue<string>("apiKey");

        public LogLevel LogLevel
        {
            get
            {
                var level = Config.GetSection("logging").GetValue<string>("loggingLevel");
                return level switch
                {
                    "Critical" => LogLevel.Critical,
                    "Error" => LogLevel.Error,
                    "Debug" => LogLevel.Debug,
                    "Information" => LogLevel.Information,
                    "Trace" => LogLevel.Trace,
                    _ => LogLevel.None,
                };
            }
        }

    }
}
