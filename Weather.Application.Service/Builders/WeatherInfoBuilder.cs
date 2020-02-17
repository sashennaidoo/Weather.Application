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
                                  , IReadonlyRepository<City> cityRepository
                                  , IRequestFactory requestFactory)
            : base(logger,restClient, cityRepository, requestFactory)
        {
        }

        public override IFormattable Build(int cityId)
        {
            // First get the correct city by the Id passed in
            _logger.LogDebug($"Getting City from repository");
            var city = _cityRepository.Get(cityId);
            if (city is null)
            {
                _logger.LogError($"City Not Found for id {cityId}");
                throw new WeatherBuilderException($"City not found for Id : {cityId}");
            }
            _logger.LogDebug($"Creating Weather request");
            var request = _requestFactory.CreateRequest(city.Name);
            // First we want to build
            _logger.LogDebug($"Builder start for Weather Info");
            try
            {
                _logger.LogDebug($"Calling Weather Api");
                var response = _restClient.Get(request.GetKeyValuePairs(), _endpoint);
                _logger.LogDebug($"Call Success - Getting user friendly information from response");
                return response.Result.Information;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogCritical($"WeatherDetailBuilder Exception during request {ex.Message}");
                throw new WeatherBuilderException("Exception occured while attempting Api call", ex);
            }
            catch(AggregateException aeg)
            {
                _logger.LogError($"WeatherDetailBuilder Exception during request {aeg.Message}");
                throw new WeatherBuilderException("Exception occured while attempting Api call", aeg);
            }
        }

        public override string BuildAndFormat(int cityId)
        {
            try
            {
                _logger.LogDebug($"Executing Build with Id");
                var weatherDetail = Build(cityId);
                _logger.LogDebug("Formatting into nicer format");
                return weatherDetail.ToString("F", null);
            }
            catch (WeatherBuilderException wex)
            {
                _logger.LogCritical($"Unable to process request due to exception : {wex}");
                throw wex;
            }
            catch (WeatherFormatException wfex)
            {
                _logger.LogCritical($"Unable to process request due to exception : {wfex}");
                throw wfex;
            }
        }
    }
}
