using System.ComponentModel.DataAnnotations;
using Transactions.API.Entities;

namespace Transactions.API.DTOs.Request;

public record struct FrequentTransactionCreateRequestDto
{
    [Required(AllowEmptyStrings = false), Display(Name = nameof(Title))]
    public string? Title { get; init; }

    [Required, Range(0, 1, ErrorMessage = "Enter a valid Type"), Display(Name = nameof(Type))]
    public TransactionType? Type { get; init; }

    [Required, Range(0.01, 10_000_000), Display(Name = nameof(Amount))]
    public decimal? Amount { get; init; }

    [Required, Range(0, 6, ErrorMessage = "Enter a valid Category"), Display(Name = nameof(Category))]
    public TransactionCategory? Category { get; init; }
}