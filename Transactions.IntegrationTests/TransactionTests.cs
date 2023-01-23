using Transactions.API.DTOs.Request;
using Transactions.API.DTOs.Response;
using Transactions.API.Helpers;
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
    public async Task UT1_CreationSuccess()
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
    public async Task UT2_CreationValidationError()
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
    public async Task UT3_UpdateSuccess()
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

    [Fact(DisplayName = "(4) Transaction: Validation errors in update")]
    public async Task UT4_UpdateValidationError()
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

    [Fact(DisplayName = "(5) Transaction: Success in Get")]
    public async Task UT5_GetSuccess()
    {
        //Arrange
        var period = "012024";

        //Act
        var result = await _application.Get<ApiResult<DashboardResponseDTO>>(period);

        //Assert
        Assert.True(result.Success);
        Assert.Equal((int)OK, result.StatusCode);
        Assert.Equal(5, result.Data.Transactions.Count);
        Assert.Equal(113.94M, result.Data.Outcome.Total);
        Assert.Equal(313.94M, result.Data.Income.Total);
        Assert.Equal(427.88M, result.Data.Sum.Total);
    }

    [Fact(DisplayName = "(7) Transaction: Success in delete")]
    public async Task UT7_DeleteSuccess()
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

    [Fact(DisplayName = "(8) Transaction: Validation errors in delete")]
    public async Task UT8_DeleteValidationError()
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