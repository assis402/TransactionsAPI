namespace Transactions.IntegrationTests;

public class TransactionTests : IClassFixture<TransactionsApplication>
{
    private readonly TransactionsApplication _application;

    public TransactionTests(TransactionsApplication application)
        => _application = application;

    [Fact(DisplayName = "Transaction: Success in creation")]
    public async Task Test1()
    {
        //Arrange
        var client =_application.CreateClient();

        //Act
        var result = await client.GetAsync("/transaction");
        var response = await result.Content.ReadAsStringAsync();

        //Assert
    }
}