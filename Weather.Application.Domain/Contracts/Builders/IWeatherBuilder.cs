using System;
using Weather.Application.Domain.Dto.Request;

namespace Weather.Application.Domain.Contracts.Builders
{
    public interface IWeatherBuilder
    {
        IFormattable Build(int cityId);
        string BuildAndFormat(int cityId);
    }
}
