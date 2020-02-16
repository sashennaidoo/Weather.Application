using System;
using Newtonsoft.Json;

namespace Weather.Application.Domain.Dto
{
    public class Fall
    {
        [JsonProperty(PropertyName = "1h")]
        public int LastHour { get; private set; }

        [JsonProperty(PropertyName = "3h")]
        public int LastThreeHours { get; private set; }
    }
}
