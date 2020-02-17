using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Weather.Application.Domain.Exceptions;
using Weather.Application.Domain.Resolvers;

namespace Weather.Application.Domain.Dto
{
    public class WeatherDetails : IFormattable
    {
        [JsonProperty(PropertyName = "timezone")]
        public int TimeZoneShiftInSeconds { get; private set; }

        [JsonProperty(PropertyName = "id")]
        public int CityId { get; private set; }

        [JsonProperty(PropertyName = "name")]
        public string CityName { get; private set; }

        [JsonProperty(PropertyName = "cod")]
        public int Code { get; private set; }

        [JsonProperty(PropertyName = "dt")]
        public string Time { get; private set; }

        [JsonProperty(PropertyName = "coords")]
        public Coordinates Coordinates { get; private set; }

        [JsonProperty(PropertyName = "weather")]
        public List<WeatherCondition> Conditions { get; private set; }

        [JsonProperty(PropertyName = "main")]
        public WeatherInfo Information { get; private set; }

        [JsonProperty(PropertyName = "wind")]
        public Wind Wind { get; private set; }

        [JsonProperty(PropertyName = "rain")]
        public Fall RainFall { get; private set; }

        [JsonProperty(PropertyName = "snow")]
        public Fall SnowFall { get; private set; }

        [JsonProperty(PropertyName = "sys")]
        public System SystemInformation { get; private set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            formatProvider = formatProvider ?? CultureInfo.CurrentCulture;
            if (format == "R")
                return ToRawJson();
            else if (format == "F")
                return ToFormattedString(this, 0);
            else
            { throw new WeatherFormatException("Invalid Format"); }
                
        }

        private string ToRawJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new JsonResolver() });
        }

        public string ToFormattedString(object obj, int indent)
        {
            if (obj == null) return string.Empty;
            var stringBuilder = new StringBuilder();
            string indentString = new string(' ', indent);
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj, null);
                var elems = propValue as IList;
                if (elems != null)
                {
                    foreach (var item in elems)
                    {
                        stringBuilder.AppendLine(ToFormattedString(item, indent + 3));
                    }
                }
                else
                {
                    // This will not cut-off System.Collections because of the first check
                    if (property.PropertyType.Assembly == objType.Assembly)
                    {
                        if (propValue  != null)
                        {
                            stringBuilder.AppendLine($"{indentString} {property.Name}");
                            stringBuilder.AppendLine(ToFormattedString(propValue, indent + 3));
                        }
                    }
                    else
                    {
                        stringBuilder.AppendLine($"{indentString}{property.Name}: {propValue}");
                    }
                }
            }
            return stringBuilder.ToString();
        }
        
    }
}