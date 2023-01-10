using System.ComponentModel.DataAnnotations;
using TransactionsAPI.Entity;

namespace TransactionsAPI.DTOs;

public record struct TransactionResponseDTO
{
    public string Id { get; init; }
    public string Title { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; init; }
    public TransactionCategory Category { get; init; }
    public string? Period { get; init; }
    public DateTime Date { get; init; }

    public static implicit operator TransactionResponseDTO(Transaction transaction)
        => new TransactionResponseDTO
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
