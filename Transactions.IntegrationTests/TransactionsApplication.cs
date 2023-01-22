using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using Transactions.API.DTOs.Request;
using Transactions.API.DTOs.Response;
using Transactions.API.Entities;
using Transactions.API.Helpers;
using static System.Net.Mime.MediaTypeNames;
using static Transactions.IntegrationTests.Helpers.TransactionHelper;

namespace Transactions.IntegrationTests;

public class TransactionsApplication : WebApplicationFactory<Program>, IDisposable
{
    private readonly HttpClient _httpClient;
    private const string _baseUrl = "https://localhost:5098/transaction";

    public TransactionsApplication()
    {
        _httpClient = CreateClient(_baseUrl);
        CreateBaseTestAsync().GetAwaiter();
    }

    private async Task CreateBaseTestAsync()
    {
        var income = GenerateCreateRequest(TransactionType.Income);
        var outcome = GenerateCreateRequest(TransactionType.Income);
        
        await Post(income);
        await Post(income);
        await Post(outcome);
        await Post(outcome);
    }

    public void Dispose()
    {
        //do something
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTests");
        return base.CreateHost(builder);
    }

    private HttpClient CreateClient(string url)
    {
        var httpClientOptions = new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri(url)
        };

        return CreateClient(httpClientOptions);
    }

    public async Task<TResponse> Get<TResponse>(string? period)
    {
        var result = await _httpClient.GetAsync($"{_httpClient.BaseAddress}?period={period}");
        var responseString = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseString);
    }

    public async Task Post<TRequest>(TRequest request) 
        => await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress, request);

    public async Task<TResponse> Post<TRequest, TResponse>(TRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress, request);
        var responseString = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseString);
    }

    public async Task<TResponse> Put<TRequest, TResponse>(TRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress, request);
        var responseString = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseString);
    }

    public async Task<TResponse> Delete<TResponse>(string? id)
    {
        var result = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}?id={id}");
        var responseString = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseString);
    }
}