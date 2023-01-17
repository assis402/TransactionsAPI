using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Transactions.API.DTOs.Request;
using Transactions.API.DTOs.Response;
using Transactions.API.Helpers;
using Transactions.IntegrationTests.Helpers;
using static System.Net.HttpStatusCode;
using static Transactions.IntegrationTests.Helpers.TransactionHelper;

namespace Transactions.IntegrationTests;

public class TransactionTests : IClassFixture<TransactionsApplication>
{
    private readonly TransactionsApplication _application;
    private readonly HttpClient _httpClient;
    private const string _baseUrl = "https://localhost:5098/transaction";

    public TransactionTests(TransactionsApplication application)
    {
        _application = application;
        _httpClient = _application.CreateClient(_baseUrl);
    }

    [Fact(DisplayName = "Transaction: Success in creation")]
    public async Task Test1()
    {
        //Arrange
        var request = GenerateRequest();

        //Act
        var result = await _httpClient.Post<TransactionCreateRequestDTO,
                                            ApiResult<TransactionResponseDTO>>(request);

        //Assert
        Assert.True(result.Success);
        Assert.Equal(OK, result.StatusCode);
    }
}