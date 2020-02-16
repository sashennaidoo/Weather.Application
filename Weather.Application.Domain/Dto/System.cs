using System;
using Newtonsoft.Json;
using Weather.Application.Domain.Extensions.Converters;

namespace Weather.Application.Domain.Dto
{
    public class System
    {
        [JsonIgnore]
        public int SystemType { get; private set; }

        [JsonIgnore]
        public int Id { get; private set; }

        [JsonIgnore]
        public int Message { get; private set; }

        [JsonProperty(PropertyName = "country")]
        public string CountryCode { get; private set; }

        [JsonProperty(PropertyName = "sunrise")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime Sunrise { get; private set; }

        [JsonProperty(PropertyName = "sunset")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime Sunset { get; private set; }
    }
}