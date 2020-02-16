using Microsoft.Extensions.Logging;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Contracts.Repository;
using Weather.Application.Domain.Dto;

namespace Weather.Application.Service.Builders
{
    public class WeatherRawDetailBuilder : WeatherBuilderBase
    {
        public WeatherRawDetailBuilder(ILogger logger
                                        , IRestClient<WeatherDetails> restClient
                                        , IReadRepository<City> cityRepository
                                        , IRequestFactory requestFactory)
            : base(logger, restClient, cityRepository, requestFactory)
        {
        }


        public override string BuildAndFormat(int cityId)
        {
            var weatherDetail = Build(cityId);

            return weatherDetail.ToString("R", null);
        }
    }

}
