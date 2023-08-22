using Transactions.Business.Enums;
using Transactions.Shared.DTOs.Request;
using Transactions.Shared.DTOs.Response;

namespace Transactions.Business.Entities;

public class Transaction : BaseEntity
{
    public string Title { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionCategory Category { get; private set; }
    public string Period { get; private set; }
    public DateTime Date { get; private set; }

    public Transaction(TransactionCreateRequestDTO newTransaction) : base()
    {
        Title = newTransaction.Title;
        Type = Enum.Parse<TransactionType>(newTransaction.Type, true);
        Category = Enum.Parse<TransactionCategory>(newTransaction.Category, true);
        Amount = newTransaction.Amount;
        Date = newTransaction.Date;
        Period = GetPeriod(newTransaction.Date);
    }

    public static implicit operator TransactionResponseDTO(Transaction transaction)
    => new()
    {
        Id = transaction.Id.ToString(),
        Title = transaction.Title,
        Type = transaction.Type.ToString(),
        Amount = transaction.Amount,
        Period = transaction.Period,
        Category = transaction.Category.ToString(),
        Date = transaction.Date
    };

    private static string GetPeriod(DateTime date) => $"{date.Month:D2}{date.Year:D4}";
}