using System;
using System.Collections;
using System.Collections.Generic;

namespace Weather.Application.Domain.Dto.Request
{
    public class WeatherRequest : AbstractRequest
    {
        public string City { get; set; }
    }
}
