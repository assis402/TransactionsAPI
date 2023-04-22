using Transactions.API.DTOs.Response;
using Transactions.API.Entities;

namespace Transactions.API.Converters;

public static class TransactionConverters
{
    public static List<TransactionResponseDto> ConvertToDto(this List<Transaction> transactions)
        => transactions.Select(_ => (TransactionResponseDto)_).ToList();
}