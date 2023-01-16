using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Transactions.API.DTOs.Request;
using Transactions.API.DTOs.Response;

namespace Transactions.IntegrationTests;

public class TransactionTests : IClassFixture<TransactionsApplication>
{
    private readonly TransactionsApplication _application;
    private readonly HttpClient _httpClient;
    private const string _baseUrl = "transaction";

    public TransactionTests(TransactionsApplication application)
    {
        _application = application;
        _httpClient = _application.CreateClient();
    }

    [Fact(DisplayName = "Transaction: Success in creation")]
    public async Task Test1()
    {
        //Arrange
        var request = new TransactionCreateRequestDTO();

        //Act
        var result = await _httpClient.PostAsJsonAsync(_baseUrl, request);
        var response = await result.Content.ReadAsStringAsync();

        //Assert
    }
}