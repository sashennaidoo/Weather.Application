using System;
using System.IO;
using Microsoft.Extensions.Configuration;

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

    }
}
