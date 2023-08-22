namespace Transactions.Shared.DTOs.Request;

public class TransactionCreateRequestDTO
{
    public string Title { get; init; }

    public string Type { get; init; }

    public decimal Amount { get; init; }

    public string Category { get; init; }

    public DateTime Date { get; init; }
}