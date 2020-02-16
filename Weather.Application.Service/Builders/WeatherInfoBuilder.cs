using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Contracts.Repository;
using Weather.Application.Domain.Dto;
using Weather.Application.Domain.Exceptions;

namespace Weather.Application.Service.Builders
{
    public class WeatherInfoBuilder : WeatherBuilderBase
    {
        public WeatherInfoBuilder(ILogger logger
                                  , IRestClient<WeatherDetails> restClient
                                  , IReadRepository<City> cityRepository
                                  , IRequestFactory requestFactory)
            : base(logger,restClient, cityRepository, requestFactory)
        {
        }

        public override IFormattable Build(int cityId)
        {
            // First get the correct city by the Id passed in
            var city = _cityRepository.Get(cityId);
            if (city is null)
            {
                _logger.LogError($"City Not Found for id {cityId}");
                return null;
            }

            var request = _requestFactory.CreateRequest(city.Name);
            // First we want to build
            _logger.LogDebug($"Builder start for Weather Info");
            try
            {
                var response = _restClient.Get(request.GetKeyValuePairs(), _endpoint);
                return response.Result.Information;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogCritical($"WeatherDetailBuilder Exception during request {ex.Message}");
                throw new WeatherBuilderException("Exception occured while attempting Api call", ex);
            }
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
            catch (Exception ex)
            {
                _logger.LogCritical($"Unable to process request due to exception : {ex}");
                throw;
            }
        }
    }
}
