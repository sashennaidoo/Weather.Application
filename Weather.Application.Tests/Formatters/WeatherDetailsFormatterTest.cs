using System;
using NUnit.Framework;
using Weather.Application.Domain.Exceptions;
using Weather.Application.Tests.Factories;

namespace Weather.Application.Tests.Formatters
{
    public class WeatherDetailsFormatterTest
    {
        private IFormattable _weatherDetails;

        [SetUp]
        public void SetUp()
        {
            _weatherDetails = MockWeatherDetailsFactory.CreateMockWeatherDetails();
        }

        [TestCase("F")]
        [TestCase("R")]
        public void TestToString_WithWellFormedObject_ShouldReturnString(string format)
        {
            var formatted = _weatherDetails.ToString(format, null);
            Assert.IsInstanceOf<string>(formatted);
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestToString_WithInvalidFormat_ShouldThrowWeatherFormatException(string format)
        {
            try
            {
                _weatherDetails.ToString(format, null);
                Assert.Fail("No exception thrown");
            }
            catch(WeatherFormatException wef)
            {
                Assert.Pass($"Exception Message: {wef.Message}");
            }
        }
    }
}
