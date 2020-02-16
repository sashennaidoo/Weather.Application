using System;
using Newtonsoft.Json;

namespace Weather.Application.Domain.Dto
{
    public class Coordinates
    {
        [JsonProperty(PropertyName = "lon")]
        public decimal Longitude { get ; private set; }

        [JsonProperty(PropertyName = "lat")]
        public decimal Latitude { get; private set; }
    }
}
