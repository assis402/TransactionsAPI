using Transactions.API.DTOs.Response;
using Transactions.API.Entities;

namespace Transactions.API.Converters;

public static class TransactionConverters 
{
    public static List<TransactionResponseDTO> ConvertToDTO(this List<Transaction> transactions)
        => transactions.Select(_ => (TransactionResponseDTO)_).ToList();
}