using System;
namespace Weather.Application.Domain.Exceptions
{
    public class WeatherBuilderException : Exception
    {
        public WeatherBuilderException(string message) : base(message)
        { }

        public WeatherBuilderException(string message, Exception inner): base(message, inner)
        { }
    }
}
