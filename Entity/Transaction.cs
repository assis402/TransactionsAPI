using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TransactionsAPI.DTOs;

namespace TransactionsAPI.Entity;

public class Transaction
{
    [BsonId]
    public ObjectId Id { get; private set; }
    public string? Title { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionCategory Category { get; private set; }
    public string? Period { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? UpdateDate { get; private set; }

    public Transaction(TransactionCreateRequestDTO newTransaction)
    {
        Id = ObjectId.GenerateNewId();
        Title = newTransaction.Title;
        Type = newTransaction.Type!.Value;
        Category = newTransaction.Category!.Value;
        Date = newTransaction.Date!.Value;;
        Period = $"{newTransaction.Date!.Value.Month:D2}{newTransaction.Date!.Value.Year:D4}";
        CreationDate = DateTime.Now;
    }

    public Transaction(TransactionUpdateRequestDTO updatedTransaction)
    {
        Title = updatedTransaction.Title;
        Type = updatedTransaction.Type!.Value;
        Category = updatedTransaction.Category!.Value;
        Date = updatedTransaction.Date!.Value;
        Period = $"{updatedTransaction.Date!.Value.Month:D2}{updatedTransaction.Date!.Value.Year:D4}";
        UpdateDate = DateTime.Now;
    }
}