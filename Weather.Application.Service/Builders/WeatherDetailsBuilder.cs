using System;
using Microsoft.Extensions.Logging;
using Weather.Application.Domain.Contracts.Builders;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Contracts.Repository;
using Weather.Application.Domain.Dto;
using Weather.Application.Domain.Dto.Request;
using Weather.Application.Domain.Exceptions;

namespace Weather.Application.Service.Builders
{
    // Builder class for the weather details object
    public class WeatherDetailsBuilder : WeatherBuilderBase
    {

        public WeatherDetailsBuilder(ILogger logger
                                    , IRestClient<WeatherDetails> restClient
                                    , IReadonlyRepository<City> cityRepository
                                    , IRequestFactory requestFactory) : base(logger,restClient,cityRepository,requestFactory)
        {
        }

        public override string BuildAndFormat(int cityId)
        {
            try
            {
                _logger.LogDebug($"WeatherDetailsBuilder - Executing Build with request");
                var weatherDetail = Build(cityId);
                _logger.LogDebug("Formatting into nicer format");
                return weatherDetail.ToString("F", null);
            }
            catch(WeatherBuilderException wex)
            {
                _logger.LogCritical($"Unable to process request due to exception : {wex}");
                throw wex;
            }
            catch(WeatherFormatException wfex)
            {
                _logger.LogError($"Unable to process request due to exception : {wfex}");
                throw wfex;
            }
            
        }
    }
}
