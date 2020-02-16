using System;
using Microsoft.Extensions.Logging;
using Weather.Application.Domain.Contracts.Builders;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Contracts.Repository;
using Weather.Application.Domain.Dto;
using Weather.Application.Domain.Enums;
using Weather.Application.Service.Builders;

namespace Weather.Application.Service.Factories
{
    public class WeatherBuilderFactory : IWeatherBuilderFactory
    {
        private ILogger _logger;
        private IRestClient<WeatherDetails> _restClient;
        private IReadRepository<City> _cityRepository;
        private IRequestFactory _requestFactory;

        public WeatherBuilderFactory(ILogger logger
                                    , IRestClient<WeatherDetails> restClient
                                    , IReadRepository<City> cityRepository
                                    , IRequestFactory requestFactory)
        {
            _logger = logger;
            _restClient = restClient;
            _cityRepository = cityRepository;
            _requestFactory = requestFactory;
        }

        public IWeatherBuilder Create(WeatherDisplayType displayType) => displayType switch
        {
            WeatherDisplayType.Details => new WeatherDetailsBuilder(_logger, _restClient, _cityRepository, _requestFactory),
            WeatherDisplayType.Info => new WeatherInfoBuilder(_logger, _restClient, _cityRepository, _requestFactory),
            WeatherDisplayType.Raw => new WeatherRawDetailBuilder(_logger, _restClient, _cityRepository, _requestFactory),
            _ => new WeatherDetailsBuilder(_logger,_restClient, _cityRepository, _requestFactory)
        };
    }
}
