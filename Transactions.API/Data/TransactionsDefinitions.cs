using MongoDB.Bson;
using MongoDB.Driver;
using Transactions.API.Entities;

namespace Transactions.API.Data;

public static class TransactionsDefinitions {
    public static UpdateDefinition<Transaction> UpdateDefinition(Transaction transaction)
        => Builders<Transaction>.Update.Set(nameof(transaction.Title).ToLower(), transaction.Title)
                                       .Set(nameof(transaction.Amount).ToLower(), transaction.Amount)
                                       .Set(nameof(transaction.Type).ToLower(), transaction.Type)
                                       .Set(nameof(transaction.Category).ToLower(), transaction.Category)
                                       .Set(nameof(transaction.Date).ToLower(), transaction.Date);
    
    public static FilterDefinition<Transaction> GetByIdFilterDefinition(string id)
        => Builders<Transaction>.Filter.Eq("_id", ObjectId.Parse(id));

    public static FilterDefinition<Transaction> GetByPeriodFilterDefinition(string period)
        => Builders<Transaction>.Filter.Eq(nameof(period), period);  
}