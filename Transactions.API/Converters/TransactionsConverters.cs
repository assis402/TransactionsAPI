using Transactions.API.DTOs.Response;
using Transactions.API.Entities;

namespace Transactions.API.Converters;

public static class TransactionConverters 
{
    public static List<TransactionResponseDTO> ConvertToResult(this List<Transaction> transactions)
        => transactions.Select(_ => (TransactionResponseDTO)_).ToList();
}