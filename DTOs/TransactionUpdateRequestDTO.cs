using System.ComponentModel.DataAnnotations;
using TransactionsAPI.Entity;

namespace TransactionsAPI.DTOs;

public record struct TransactionUpdateRequestDTO
{
    [Required(AllowEmptyStrings=false), Display(Name = nameof(Id))]
    public string? Id { get; init; }

    [Required(AllowEmptyStrings=false), Display(Name = nameof(Title))]
    public string? Title { get; init; }

    [Required, Display(Name = nameof(Type))]
    public TransactionType? Type { get; init; }

    [Required, Range(0.01, 10_000_000), Display(Name = nameof(Amount))]
    public decimal? Amount { get; init; }

    [Required, Display(Name = nameof(Category))]
    public TransactionCategory? Category { get; init; }

    [Required, Display(Name = nameof(Date))]
    public DateTime? Date { get; init; }
}
