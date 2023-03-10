using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Transactions.API.Entities;

public class FrequentTransaction
{
    [BsonId]
    public ObjectId Id { get; private set; }

    public string? Title { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionCategory Category { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? UpdateDate { get; private set; }

    // public Transaction(TransactionCreateRequestDTO newTransaction)
    // {
    //     Id = ObjectId.GenerateNewId();
    //     Title = newTransaction.Title;
    //     Type = newTransaction.Type!.Value;
    //     Category = newTransaction.Category!.Value;
    //     Date = newTransaction.Date!.Value;
    //     Period = GetPeriod(newTransaction.Date!.Value);
    //     CreationDate = DateTime.Now;
    // }

    // public Transaction(TransactionUpdateRequestDTO updatedTransaction)
    // {
    //     Id = new ObjectId(updatedTransaction.Id);
    //     Title = updatedTransaction.Title;
    //     Type = updatedTransaction.Type!.Value;
    //     Category = updatedTransaction.Category!.Value;
    //     Date = updatedTransaction.Date!.Value;
    //     Period = GetPeriod(updatedTransaction.Date!.Value);
    //     UpdateDate = DateTime.Now;
    // }
}