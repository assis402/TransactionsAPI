using Transactions.API.DTOs.Request;
using Transactions.API.Entities;

namespace Transactions.IntegrationTests.Helpers;

internal static class TransactionHelper
{
    internal static TransactionCreateRequestDTO GenerateCreateRequest()
        => new ()
        {
            Title = "title",
            Type = TransactionType.Outcome,
            Amount = 100,
            Category = TransactionCategory.Purchases,
            Date = new DateTime(2023, 01, 01)
        };

    internal static TransactionUpdateRequestDTO GenerateUpdateRequest(string id)
        => new ()
        {
            Id = id,
            Title = "title novo",
            Type = TransactionType.Income,
            Amount = 200,
            Category = TransactionCategory.Food,
            Date = new DateTime(2024, 01, 01)
        };
}