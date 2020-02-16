using System;
using Microsoft.Extensions.Logging;
using Weather.Application.Domain.Contracts.Builders;
using Weather.Application.Domain.Dto.Request;
using Weather.Application.Domain.Enums;

namespace Weather.Application.Service.Builders
{
    public class WeatherBuilderDirectory
    {
        // Good place to put the function call for our correct builder as
        // this should be the directors job
        private readonly Func<WeatherDisplayType, IWeatherBuilder> _weatherBuilderFunc;
        private readonly ILogger _logger;

        public WeatherBuilderDirectory(ILogger logger, Func<WeatherDisplayType, IWeatherBuilder> weatherFunc)
        {
            _logger = logger;
            _weatherBuilderFunc = weatherFunc;
        }

        public string BuildAndFormat(WeatherDisplayType displayType, AbstractRequest request) => displayType switch
        {            WeatherDisplayType.Raw => _weatherBuilderFunc.Invoke(WeatherDisplayType.Details)
                                                         .Build(request).ToString("RawJson", null),
            WeatherDisplayType.Details => _weatherBuilderFunc.Invoke(WeatherDisplayType.Details)
                                                         .Build(request).ToString("Neat", null),
            WeatherDisplayType.Info => _weatherBuilderFunc.Invoke(WeatherDisplayType.Info)
                                                         .Build(request).ToString("Neat", null)
        };
    }
}
