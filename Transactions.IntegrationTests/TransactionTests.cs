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
    private string _transactionId;

    public TransactionTests(TransactionsApplication application)
    {
        _application = application;
        _httpClient = _application.CreateClient(_baseUrl);
    }

    [Fact(DisplayName = "Transaction: Success in creation")]
    public async Task CreationSuccess()
    {
        //Arrange
        var request = GenerateCreateRequest();

        //Act
        var result = await _httpClient.Post<TransactionCreateRequestDTO,
                                            ApiResult<TransactionResponseDTO>>(request);
        _transactionId = result.Data.Id;

        //Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);

        Assert.Equal(request.Title, result.Data.Title);
        Assert.Equal(request.Amount, result.Data.Amount);
        Assert.Equal(request.Category, result.Data.Category);
        Assert.Equal(request.Type, result.Data.Type);
        Assert.Equal(request.Date, result.Data.Date);
        Assert.Equal("012023", result.Data.Period);
        Assert.Equal((int)OK, result.StatusCode);
    }

    [Fact(DisplayName = "Transaction: Validation errors in creation")]
    public async Task CreationValidationError()
    {
        //Arrange
        var request = new TransactionCreateRequestDTO();

        //Act
        var result = await _httpClient.Post<TransactionCreateRequestDTO,
                                            ApiResult<IDictionary<string, string[]>>>(request);

        //Assert
        Assert.False(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal((int)BadRequest, result.StatusCode);
    }

    [Fact(DisplayName = "Transaction: Success in update")]
    public async Task UpdateSuccess()
    {
        //Arrange
        var request = GenerateUpdateRequest(_transactionId);

        //Act
        var result = await _httpClient.Post<TransactionUpdateRequestDTO,
                                            ApiResult<TransactionResponseDTO>>(request);

        //Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);

        Assert.Equal(_transactionId, result.Data.Id);
        Assert.Equal(request.Title, result.Data.Title);
        Assert.Equal(request.Amount, result.Data.Amount);
        Assert.Equal(request.Category, result.Data.Category);
        Assert.Equal(request.Type, result.Data.Type);
        Assert.Equal(request.Date, result.Data.Date);
        Assert.Equal("012024", result.Data.Period);
        Assert.Equal((int)OK, result.StatusCode);
    }
}