using Microsoft.Extensions.Logging;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Contracts.Repository;
using Weather.Application.Domain.Dto;
using Weather.Application.Domain.Exceptions;

namespace Weather.Application.Service.Builders
{
    public class WeatherRawDetailBuilder : WeatherBuilderBase
    {
        public WeatherRawDetailBuilder(ILogger logger
                                        , IRestClient<WeatherDetails> restClient
                                        , IReadonlyRepository<City> cityRepository
                                        , IRequestFactory requestFactory)
            : base(logger, restClient, cityRepository, requestFactory)
        {
        }


        public override string BuildAndFormat(int cityId)
        {
            try
            {
                var weatherDetail = Build(cityId);
                return weatherDetail.ToString("R", null);
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
