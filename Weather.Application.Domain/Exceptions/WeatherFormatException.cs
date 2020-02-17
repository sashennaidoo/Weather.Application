using System;
namespace Weather.Application.Domain.Exceptions
{
    public class WeatherFormatException : Exception
    {
        public WeatherFormatException(string message) : base(message)
        {
        }

        public WeatherFormatException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
