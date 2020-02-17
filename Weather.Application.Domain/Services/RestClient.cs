using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Extensions.Converters;
using Weather.Application.Domain.Resolvers;

namespace Weather.Application.Domain.Services
{
    public class RestClient<T> : IRestClient<T>
    {
        private HttpClient _restclient;
        private string _apiKey;
        
        public RestClient(string baseUrl, string apiKey)
        {
            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out Uri baseUri))
                throw new ArgumentException("Invalid Uri");

            _apiKey = apiKey;
            _restclient = new HttpClient
            {
                BaseAddress = baseUri,
            };
            
        }

        public async void Delete(string endpoint)
        {
            try
            {
                var response = await _restclient.DeleteAsync(new Uri(_restclient.BaseAddress, endpoint));
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException requestEx)
            {
                // Todo : Handle Exception
                throw requestEx;
            }
        }

        public async Task<T> Get(Dictionary<string, object> queryParams, string endpoint = null)
        {
            var queryBuilder = new UriBuilder(new Uri(_restclient.BaseAddress, endpoint));

            if (queryParams != null)
            {
                foreach (var key in queryParams.Keys)
                {
                    var query = HttpUtility.ParseQueryString(queryBuilder.Query);
                    if (queryParams.TryGetValue(key, out object val))
                    {
                        query[key] = val.ToString();
                    }
                    query["appId"] = _apiKey;
                    queryBuilder.Query = query.ToString();
                }
            }

            try
            {
                var result = await _restclient.GetAsync(queryBuilder.Uri);
                // Check if we recieved an OK back from the server
                result.EnsureSuccessStatusCode();
                // Create a StreamReader to read the body of the response
                using var stream = await result.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(stream);
                var data = streamReader.ReadToEnd();
                // Deserialize using our custom resolver and time converter
                return JsonConvert.DeserializeObject<T>(data
                                                        , new JsonSerializerSettings
                                                        {
                                                            ContractResolver = new JsonResolver()
                                                            , Converters = new List<JsonConverter> { new SecondEpochConverter() }
                                                        }
                                                        );

            }
            catch (HttpRequestException requestException)
            {
                throw requestException;
            }
            catch (AggregateException aeg)
            {
                throw aeg;
            }
        }

        public async void Post(T toPost, string endpoint = null)
        {
            if (toPost is null)
                throw new ArgumentNullException("No Post Data");
            // Set up the post object to HttpContent type to be sent with request
            var content = new StringContent(JsonConvert.SerializeObject(toPost), Encoding.UTF8);
            try
            {
                var response = await _restclient.PostAsync(new Uri(_restclient.BaseAddress, endpoint), content);
                // Check if we recieved an OK back from the server
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException requestEx)
            {
                throw requestEx;
            }
        }

        public async Task<T> Put(T toPut, string endpoint = null)
        {
            if (toPut is null)
                throw new ArgumentNullException("No data to put");
            // Set up the post object to HttpContent type to be sent with request
            var content = new StringContent(JsonConvert.SerializeObject(toPut), Encoding.UTF8);

            try
            {
                var response = await _restclient.PutAsync(new Uri(_restclient.BaseAddress, endpoint), content);
                // Check if we recieved an OK back from the server
                response.EnsureSuccessStatusCode();
                // Create a StreamReader to read the body of the response
                using var stream = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(stream);
                var data = streamReader.ReadToEnd();
                // Deserialize using our custom resolver and time converter
                return JsonConvert.DeserializeObject<T>(data
                                                        , new JsonSerializerSettings
                                                        {
                                                            ContractResolver = new JsonResolver()
                                                            , Converters = new List<JsonConverter> { new SecondEpochConverter() }
                                                        }
                                                        );

            }
            catch (HttpRequestException requestException)
            {
                throw requestException;
            }
            catch (AggregateException aeg)
            {
                throw aeg;
            }

        }

    }
}
