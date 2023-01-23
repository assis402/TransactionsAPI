using Transactions.API.Entities;

namespace Transactions.API.DTOs.Response;

public readonly record struct TransactionResponseDTO
{
    public string Id { get; init; }
    public string Title { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; init; }
    public TransactionCategory Category { get; init; }
    public string? Period { get; init; }
    public DateTime Date { get; init; }

    public static implicit operator TransactionResponseDTO(Transaction transaction)
        => new()
        {
            Id = transaction.Id.ToString(),
            Title = transaction.Title!,
            Type = transaction.Type!,
            Amount = transaction.Amount,
            Period = transaction.Period,
            Category = transaction.Category,
            Date = transaction.Date
        };
}