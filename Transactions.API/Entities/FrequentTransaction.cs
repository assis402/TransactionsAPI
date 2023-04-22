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
}