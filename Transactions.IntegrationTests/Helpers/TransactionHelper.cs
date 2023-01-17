using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transactions.API.DTOs.Request;
using Transactions.API.Entities;

namespace Transactions.IntegrationTests.Helpers
{
    internal static class TransactionHelper
    {
        internal static TransactionCreateRequestDTO GenerateRequest()
            => new()
            {
                Date = DateTime.Now,
                Amount = 100,
                Category = TransactionCategory.Purchases,
                Title = "Compra",
                Type = TransactionType.Outcome
            };
    }
}
