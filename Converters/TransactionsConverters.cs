using TransactionsAPI.DTOs;
using TransactionsAPI.Entity;

namespace TransactionsAPI.Converters;

public static class TransactionConverters 
{
    public static List<TransactionResponseDTO> ConvertToResult(this List<Transaction> transactions)
        => transactions.Select(_ => (TransactionResponseDTO)_).ToList();
}