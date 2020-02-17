using System;
using Newtonsoft.Json;
using Weather.Application.Domain.Extensions.Converters;

namespace Weather.Application.Domain.Dto
{
    public class System
    {
        [JsonProperty(PropertyName = "type")]
        public int SystemType { get; private set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }

        [JsonProperty(PropertyName = "message")]
        public int Message { get; private set; }

        [JsonProperty(PropertyName = "country")]
        public string CountryCode { get; private set; }

        [JsonProperty(PropertyName = "sunrise")]
        [JsonConverter(typeof(SecondEpochConverter))]
        public DateTime Sunrise { get; private set; }

        [JsonProperty(PropertyName = "sunset")]
        [JsonConverter(typeof(SecondEpochConverter))]
        public DateTime Sunset { get; private set; }
    }
}