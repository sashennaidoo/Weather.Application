using System;
using Newtonsoft.Json;

namespace Weather.Application.Domain.Dto
{
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public decimal Speed { get; private set; }

        [JsonProperty(PropertyName = "deg")]
        public int Direction { get; private set; }
    }
}
