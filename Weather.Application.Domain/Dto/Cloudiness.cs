using System;
using Newtonsoft.Json;

namespace Weather.Application.Domain.Dto
{
    // DDD - we can extend this class to an entity and use it in an ORM
    public class Cloudiness
    {
        [JsonProperty(PropertyName = "all")]
        public int PercentageCloudiness { get; private set; }
    }
}
