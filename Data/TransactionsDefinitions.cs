using MongoDB.Bson;
using MongoDB.Driver;
using TransactionsAPI.Entity;

namespace TransactionsAPI.Data;

public static class TransactionsDefinitions {
    public static UpdateDefinition<Transaction> UpdateDefinition(Transaction transaction)
        => Builders<Transaction>.Update.Set(_ => _.Title, transaction.Title)
                                       .Set(_ => _.Amount, transaction.Amount)
                                       .Set(_ => _.Type, transaction.Type)
                                       .Set(_ => _.Category, transaction.Category)
                                       .Set(_ => _.Date, transaction.Date);
    
    public static FilterDefinition<Transaction> GetByIdFilterDefinition(string id)
        => Builders<Transaction>.Filter.Eq("_id", ObjectId.Parse(id));

    public static FilterDefinition<Transaction> GetByPeriodFilterDefinition(string period)
        => Builders<Transaction>.Filter.Eq(_ => _.Period == period, true);  
}