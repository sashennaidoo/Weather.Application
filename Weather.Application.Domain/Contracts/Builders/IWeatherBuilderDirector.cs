using System;
using Weather.Application.Domain.Dto.Request;
using Weather.Application.Domain.Enums;

namespace Weather.Application.Domain.Contracts.Builders
{
    public interface IWeatherBuilderDirector
    {
        string BuildAndFormat(WeatherDisplayType displayType, AbstractRequest request);
    }
}
