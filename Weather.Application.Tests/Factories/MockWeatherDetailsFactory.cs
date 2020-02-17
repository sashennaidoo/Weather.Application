using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Weather.Application.Domain.Dto;
using Weather.Application.Domain.Extensions.Converters;
using Weather.Application.Domain.Resolvers;

namespace Weather.Application.Tests.Factories
{
    public static class MockWeatherDetailsFactory
    {
        public static WeatherDetails CreateMockWeatherDetails()
        {
            return JsonConvert.DeserializeObject<WeatherDetails>(weatherDetailsJson(), new JsonSerializerSettings
            {
                ContractResolver = new JsonResolver()
                , Converters = new List<JsonConverter> { new SecondEpochConverter() }
            });
        }

        public static string weatherDetailsJson()
        {
            return "{\"coord\":{\"lon\":18.42,\"lat\":-33.93},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"base\":\"stations\",\"main\":{\"temp\":291.08,\"feels_like\":290.85,\"temp_min\":290.93,\"temp_max\":291.48,\"pressure\":1016,\"humidity\":93},\"visibility\":10000,\"wind\":{\"speed\":3.6,\"deg\":330},\"clouds\":{\"all\":75},\"dt\":1581914834,\"sys\":{\"type\":1,\"id\":1899,\"country\":\"ZA\",\"sunrise\":1581913377,\"sunset\":1581961085},\"timezone\":7200,\"id\":3369157,\"name\":\"Cape Town\",\"cod\":200}";
        }
    }
}
