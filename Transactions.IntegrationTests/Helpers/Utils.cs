using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Transactions.IntegrationTests.Helpers;

internal static class Utils
{
    internal static async Task<TResponse> Post<TRequest, TResponse>(this HttpClient httpClient, TRequest request)
    {
        var result = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, request);
        var responseString = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseString);
    }

    internal static HttpClient CreateClient(this TransactionsApplication application, string url)
    {
        var httpClientOptions = new WebApplicationFactoryClientOptions 
        {
            BaseAddress = new Uri(url)
        };

        return application.CreateClient(httpClientOptions);
    }
}
