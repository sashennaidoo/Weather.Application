using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Weather.Application.Domain.Dto
{
    public class WeatherInfo : IFormattable
    {
        [JsonProperty(PropertyName = "temp")]
        [DisplayName("Temperature")]
        public decimal Temperature { get; private set; }

        [JsonProperty(PropertyName = "feels_like")]
        [DisplayName("Feels Like")]
        public decimal FeelsLike { get; private set; }

        [JsonProperty(PropertyName = "temp_max")]
        [DisplayName("Maximum Temperature")]
        public decimal MaximumTemperature { get; private set; }

        [JsonProperty(PropertyName = "temp_min")]
        [DisplayName("Minimum Temperature")]
        public decimal MinimumTemperature { get; private set; }

        [JsonProperty(PropertyName = "pressure")]
        [DisplayName("Pressure")]
        public int Pressure { get; private set; }

        [JsonProperty(PropertyName = "humidity")]
        [DisplayName("Humidity")]
        public int Humidity { get; private set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "F")
                return ToFormattedString();
            else
                throw new ArgumentException("Invalid format");
        }

        public string ToFormattedString()
        {
            var propertiesInfo = this.GetType().GetProperties();
            var stringBuilder = new StringBuilder();
            foreach(var info in propertiesInfo)
            {
                var displayName = info.Name;
                displayName = info.GetCustomAttributes(typeof(DisplayNameAttribute),
                false).Cast<DisplayNameAttribute>().Single().DisplayName ?? displayName;

                var value = info.GetValue(this, null) ?? string.Empty;
                stringBuilder.AppendLine($"{displayName} : {value}");
            }
            return stringBuilder.ToString();
        }
    }
}