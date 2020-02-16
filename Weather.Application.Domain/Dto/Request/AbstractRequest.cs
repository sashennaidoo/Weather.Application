using System;
using System.Collections.Generic;

namespace Weather.Application.Domain.Dto.Request
{
    public abstract class AbstractRequest
    {
        public Dictionary<string, object> GetKeyValuePairs()
        {
            var properties = this.GetType().GetProperties();
            var dictionary = new Dictionary<string, object>();
            var arguments = new List<string>();
            foreach(var property in properties)
            {
                if (property.GetValue(this, null) is null)
                    continue;
                else
                    arguments.Add(property.GetValue(this, null).ToString());
            }

            dictionary.Add("q",string.Join(',', arguments));

            return dictionary;
        }
    }
}
