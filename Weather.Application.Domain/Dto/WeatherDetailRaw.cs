using System;
using System.Text;
using Newtonsoft.Json;

namespace Weather.Application.Domain.Dto
{
    public class WeatherDetailFormatted : WeatherDetails, IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            format ??= "F";
            if (format == "F")
                return ToFormattedString(this.GetType().GetProperties());

        }

        public string ToFormattedString(PropertiesInfo[] propertiesInfo)
        {
            var stringBuilder = new StringBuilder();
            foreach (var info in propertiesInfo)
            {
                if (info.GetType().IsClass)
                    ToFormattedString(info.GetType().GetProperties());
                var value = info.GetValue(this, null) ?? string.Empty;
                stringBuilder.AppendLine($"{info.Name} : {value}");
            }
            return stringBuilder.ToString();
        }
    }
}
