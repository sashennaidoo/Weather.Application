using System;
using Newtonsoft.Json;

namespace Weather.Application.Domain.Dto
{
    // DDD - we can extend this class to an entity and use it in an ORM
    // In order to make sure callers do not modify properties
    public class WeatherCondition
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }

        [JsonProperty(PropertyName = "main")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; private set; }

        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; private set; }
    }
}
