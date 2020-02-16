using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather.Application.Domain.Contracts.Http
{
    public interface IRestClient<T>
    {
        Task<T> Get(Dictionary<string, object> queryParams, string endpoint = null);
        void Post(T toPost, string endpoint = null);
        void Delete(string endpoint);
        Task<T> Put(T toPut, string endpoint = null);
    }
}
