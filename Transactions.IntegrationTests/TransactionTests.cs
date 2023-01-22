using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Transactions.API.DTOs.Request;
using Transactions.API.DTOs.Response;
using Transactions.API.Helpers;
using Transactions.IntegrationTests.Helpers;
using static System.Net.HttpStatusCode;
using static Transactions.IntegrationTests.Helpers.TransactionHelper;

namespace Transactions.IntegrationTests;

[TestCaseOrderer(
    "Transactions.IntegrationTests.Helpers.AlphabeticalTestOrderer",
    "Transactions.IntegrationTests")]
public class TransactionTests : IClassFixture<TransactionsApplication>
{
    private readonly TransactionsApplication _application;

    public TransactionTests(TransactionsApplication application) => _application = application;

    [Fact(DisplayName = "(1) Transaction: Success in creation")]
    public async Task CreationSuccess()
    {
        //Arrange
        var request = GenerateCreateRequest();

        //Act
        var result = await _application.Post<TransactionCreateRequestDTO,
                                             ApiResult<TransactionResponseDTO>>(request);

        Environment.SetEnvironmentVariable("TEST_TRANSACTION_ID", result.Data.Id);

        //Assert
        Assert.True(result.Success);
        Assert.Equal(request.Title, result.Data.Title);
        Assert.Equal(request.Amount, result.Data.Amount);
        Assert.Equal(request.Category, result.Data.Category);
        Assert.Equal(request.Type, result.Data.Type);
        Assert.Equal(request.Date, result.Data.Date);
        Assert.Equal("012023", result.Data.Period);
        Assert.Equal((int)OK, result.StatusCode);
    }

    [Fact(DisplayName = "(2) Transaction: Validation errors in creation")]
    public async Task CreationValidationError()
    {
        //Arrange
        var request = new TransactionCreateRequestDTO();

        //Act
        var result = await _application.Post<TransactionCreateRequestDTO,
                                             ApiResult<IDictionary<string, string[]>>>(request);

        //Assert
        Assert.False(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal((int)BadRequest, result.StatusCode);
    }

    [Fact(DisplayName = "(3) Transaction: Success in update")]
    public async Task UpdateSuccess()
    {
        //Arrange
        var transactionId = Environment.GetEnvironmentVariable("TEST_TRANSACTION_ID");
        var request = GenerateUpdateRequest(transactionId!);

        //Act
        var result = await _application.Put<TransactionUpdateRequestDTO,
                                            ApiResult<TransactionResponseDTO>>(request);

        //Assert
        Assert.True(result.Success);
        Assert.Equal(transactionId, result.Data.Id);
        Assert.Equal(request.Title, result.Data.Title);
        Assert.Equal(request.Amount, result.Data.Amount);
        Assert.Equal(request.Category, result.Data.Category);
        Assert.Equal(request.Type, result.Data.Type);
        Assert.Equal(request.Date, result.Data.Date);
        Assert.Equal("012024", result.Data.Period);
        Assert.Equal((int)OK, result.StatusCode);
    }

    [Fact(DisplayName = "(5) Transaction: Validation errors in update")]
    public async Task UpdateValidationError()
    {
        //Arrange
        var request = new TransactionUpdateRequestDTO();

        //Act
        var result = await _application.Put<TransactionUpdateRequestDTO,
                                            ApiResult<IDictionary<string, string[]>>>(request);

        //Assert
        Assert.False(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal((int)BadRequest, result.StatusCode);
    }

    //[Fact(DisplayName = "(5) Transaction: Validation errors in update")]
    //public async Task GetSuccess()
    //{
    //    //Arrange
    //    var request = GenerateCreateRequest();
    //    var period = "012023";
    //    await _httpClient.Post<TransactionCreateRequestDTO,
    //                           ApiResult<TransactionResponseDTO>>(request);

    //    //Act
    //    var result = await _httpClient.Get<DashboardResponseDTO>(period);

    //    //Assert
    //    Assert.False(result.Success);
    //    Assert.NotNull(result.Data);
    //    Assert.Equal((int)BadRequest, result.StatusCode);
    //}

    [Fact(DisplayName = "(6) Transaction: Success in delete")]
    public async Task DeleteSuccess()
    {
        //Arrange
        var transactionId = Environment.GetEnvironmentVariable("TEST_TRANSACTION_ID");

        //Act
        var result = await _application.Delete<ApiResult<string>>(transactionId);

        //Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Title);
        Assert.Equal($"Transaction with id {transactionId} successfully deleted", result.Title);
        Assert.Equal((int)OK, result.StatusCode);
    }

    [Fact(DisplayName = "(7) Transaction: Validation errors in delete")]
    public async Task DeleteValidationError()
    {
        //Arrange
        var transactionId = "";

        //Act
        var result = await _application.Delete<ApiResult<string>>(transactionId);

        //Assert
        Assert.False(result.Success);
        Assert.NotNull(result.Title);
        Assert.Equal("The \"Id\" parameter is required.", result.Title);
        Assert.Equal((int)BadRequest, result.StatusCode);
    }
}