using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Contracts.Repository;
using Weather.Application.Domain.Dto;
using Weather.Application.Service.Builders;

namespace Weather.Application.Tests.Builders
{

    public class WeatherDetailsBuilderTests
    {
        private WeatherDetailsBuilder _weatherDetailsBuilder;

        #region Mocks
        private Mock<IReadonlyRepository<City>> _mockCityRepository;
        private Mock<IRestClient<WeatherDetails>> _mockRestClient;
        private Mock<ILogger> _mockLogger;
        #endregion Mocks

        public WeatherDetailsBuilderTests()
        {
            // Set up Mock Repository with defaults
            _mockCityRepository = new Mock<IReadonlyRepository<City>>();
            _mockCityRepository.Setup(c => c.Get(1)).Returns(new City { Code = 1, Name = "Cape Town" });

            // Set up Mock RestClient with defaults
            _mockRestClient = new Mock<IRestClient<WeatherDetails>>();
            _mockRestClient.Setup(c => c.Get(It.IsAny<Dictionary<string, object>>(), null)).ReturnsAsync(new WeatherDetails());
            //_weatherDetailsBuilder = new WeatherDetailsBuilder()
        }
    }
}
