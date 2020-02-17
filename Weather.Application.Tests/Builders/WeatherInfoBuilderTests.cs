using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Contracts.Repository;
using Weather.Application.Domain.Dto;
using Weather.Application.Domain.Dto.Request;
using Weather.Application.Domain.Exceptions;
using Weather.Application.Service.Builders;
using Weather.Application.Tests.Factories;

namespace Weather.Application.Tests.Builders
{
    public class AbstractWeatherDetailsBuilderTests
    {
        private WeatherInfoBuilder _weatherInfoBuilder;

        #region Mocks
        private Mock<IReadonlyRepository<City>> _mockCityRepository;
        private Mock<IRestClient<WeatherDetails>> _mockRestClient;
        private Mock<IRequestFactory> _mockRequestFactory;
        private Mock<ILogger> _mockLogger;
        #endregion Mocks

        [SetUp]
        public void SetUp()
        {
            // Set up Mock Repository with defaults
            _mockCityRepository = new Mock<IReadonlyRepository<City>>();
            _mockCityRepository.Setup(repo => repo.Get(1)).Returns(new City { Code = 1, Name = "Cape Town" });

            // Set up Mock RestClient with defaults
            _mockRestClient = new Mock<IRestClient<WeatherDetails>>();
            _mockRestClient.Setup(client => client.Get(It.IsAny<Dictionary<string, object>>(), null)).Returns(() => MockWeatherDetailsFactory.CreateMockWeatherDetails());

            // Set up Logger
            _mockLogger = new Mock<ILogger>();

            // Set up Mock Request Factory
            _mockRequestFactory = new Mock<IRequestFactory>();
            _mockRequestFactory.Setup(c => c.CreateRequest(It.IsAny<string>())).Returns(new WeatherRequest { City = "Cape Town" });
            _weatherInfoBuilder = new WeatherInfoBuilder(_mockLogger.Object, _mockRestClient.Object, _mockCityRepository.Object, _mockRequestFactory.Object);
        }

        [TestCase(1)]
        public void TestBuild_WithSetupDefaults_ShouldReturnIFormattableObject(int cityId)
        {
            var actual = _weatherInfoBuilder.Build(cityId);

            Assert.IsNotInstanceOf<IFormattable>(actual);

        }

        [TestCase(2)]
        [TestCase(0)]
        public void TestBuild_WithSetupDefault_ShouldThrowWeatherBuilderException(int cityId)
        {
            try
            {
                var data = _weatherInfoBuilder.Build(cityId);
                Assert.Fail("No exception thrown");
            }
            catch (WeatherBuilderException ex)
            {
                Assert.IsNotNull(ex);
            }
        }
    }

}
