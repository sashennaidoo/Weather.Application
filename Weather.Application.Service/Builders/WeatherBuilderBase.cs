using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Weather.Application.Domain.Contracts.Builders;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Contracts.Repository;
using Weather.Application.Domain.Dto;
using Weather.Application.Domain.Exceptions;

namespace Weather.Application.Service.Builders
{
    public abstract class WeatherBuilderBase : IWeatherBuilder
    {
        protected readonly ILogger _logger;
        protected readonly IRestClient<WeatherDetails> _restClient;
        protected readonly IReadonlyRepository<City> _cityRepository;
        protected readonly IRequestFactory _requestFactory;
        // Todo : Abstract weather into a more domain friendly place away
        protected const string _endpoint = "weather";

        public WeatherBuilderBase(ILogger logger
                                    , IRestClient<WeatherDetails> restClient
                                    , IReadonlyRepository<City> cityRepository
                                    , IRequestFactory requestFactory)
        {
            _logger = logger;
            _cityRepository = cityRepository;
            _restClient = restClient;
            _requestFactory = requestFactory;

        }

        public virtual IFormattable Build(int cityId)
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
            _logger.LogDebug($"Builder start for Weather Details");
            try
            {
                _logger.LogDebug($"Calling Weather Api");
                var response =  _restClient.Get(request.GetKeyValuePairs(), _endpoint);
                _logger.LogDebug("Call Success - Returning Weather Details Object");
                return response.Result;
            }
            catch (AggregateException ex)
            {
                _logger.LogError($"WeatherDetailBuilder Exception during request {ex.Message}");
                throw new WeatherBuilderException("Exception occurred whilst attempting Api Call", ex);
            }
        }

        public abstract string BuildAndFormat(int cityId);
    }
}
