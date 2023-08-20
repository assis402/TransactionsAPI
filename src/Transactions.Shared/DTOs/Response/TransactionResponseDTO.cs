namespace Transactions.Shared.DTOs.Response;

public class TransactionResponseDTO
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string Type { get; init; }
    public decimal Amount { get; init; }
    public string Category { get; init; }
    public string Period { get; init; }
    public DateTime Date { get; init; }
}