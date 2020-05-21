using Covid19Stats.Interface;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace Covid19Stats.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {

        public async Task<T> GetAsync(string url)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var retryPolicy = GetRetryPolicy();
                using (var httpClient = new HttpClient())
                {
                    var pollyRes = await retryPolicy.ExecuteAsync(async () =>
                    {
                        var response = await httpClient.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        return response;
                    });
                    if (pollyRes.IsSuccessStatusCode)
                    {
                        var jsonString = await pollyRes.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(jsonString);
                    }
                    else
                    {
                        throw new Exception(pollyRes.ReasonPhrase);
                    }
                }
            }
            else
            {
                throw new Exception("Internet connection is not available.");
            }
        }



        public async Task<List<T>> GetAllAsync(string url)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var retryPolicy = GetRetryPolicy();
                using (var httpClient = new HttpClient())
                {
                    var pollyRes = await retryPolicy.ExecuteAsync(async () =>
                    {
                        var response = await httpClient.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        return response;
                    });
                    if (pollyRes.IsSuccessStatusCode)
                    {
                        var jsonString = await pollyRes.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<T>>(jsonString);
                    }
                    else
                    {
                        throw new Exception(pollyRes.ReasonPhrase);
                    }
                }
            }
            else
            {
                throw new Exception("Internet connection is not available.");
            }
        }

        private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var maxRetryAttempts = 5;
            var pauseBetweenFailures = TimeSpan.FromSeconds(1);
            var retryPolicy = Policy
        .Handle<HttpRequestException>().OrResult<HttpResponseMessage>(r => (int)r.StatusCode == 429)
        .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);
            return retryPolicy;
        }
    }
}
