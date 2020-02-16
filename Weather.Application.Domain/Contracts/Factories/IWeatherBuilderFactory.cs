using System;
using Weather.Application.Domain.Contracts.Builders;
using Weather.Application.Domain.Enums;

namespace Weather.Application.Domain.Contracts.Factories
{
    public interface IWeatherBuilderFactory
    {
        IWeatherBuilder Create(WeatherDisplayType displayType);
    }
}
