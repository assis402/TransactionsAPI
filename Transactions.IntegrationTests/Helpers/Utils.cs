using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Transactions.IntegrationTests.Helpers
{
    internal static class Utils
    {
        internal static async Task<TResponse> Post<TResponse, TRequest>(this HttpClient httpClient, TRequest request)
        {
            var result = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, request);
            return JsonConvert.DeserializeObject<TResponse>(await result.Content.ReadAsStringAsync());
        }
    }
}
