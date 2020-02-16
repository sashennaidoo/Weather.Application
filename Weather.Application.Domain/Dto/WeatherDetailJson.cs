using System;
namespace Weather.Application.Domain.Dto
{
    public class WeatherDetailJson : WeatherDetails, IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            format ??= "J";
            formatProvider = formatProvider ?? CultureInfo.CurrentCulture;
            if (format == "J")
                return ToRawJson();

        }

        private string ToRawJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
