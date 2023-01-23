using Transactions.API.DTOs.Request;
using Transactions.API.Entities;

namespace Transactions.IntegrationTests.Helpers;

internal static class TransactionHelper
{
    internal static TransactionCreateRequestDTO GenerateCreateRequest()
        => new()
        {
            Title = "title",
            Type = TransactionType.Outcome,
            Amount = 100,
            Category = TransactionCategory.Purchases,
            Date = new DateTime(2023, 01, 01)
        };

    internal static TransactionCreateRequestDTO GenerateCreateRequest(TransactionType type)
        => new()
        {
            Title = "title",
            Type = type,
            Amount = 56.97M,
            Category = TransactionCategory.Purchases,
            Date = new DateTime(2024, 01, 01)
        };

    internal static TransactionCreateRequestDTO GenerateCreateRequest(DateTime date)
        => new()
        {
            Title = "title",
            Type = TransactionType.Outcome,
            Amount = 100,
            Category = TransactionCategory.Purchases,
            Date = date
        };

    internal static TransactionUpdateRequestDTO GenerateUpdateRequest(string id)
        => new()
        {
            Id = id,
            Title = "title novo",
            Type = TransactionType.Income,
            Amount = 200,
            Category = TransactionCategory.Food,
            Date = new DateTime(2024, 01, 01)
        };
}