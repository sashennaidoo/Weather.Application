using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Dto.Request;

namespace Weather.Application.Service.Factories
{
    public class RequestFactory : IRequestFactory
    {
        public AbstractRequest CreateRequest(string city)
        {
            return new WeatherRequest
            {
                City = city
            };
        }
    }
}
