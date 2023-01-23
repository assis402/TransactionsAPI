using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Transactions.API.Entities;

namespace Transactions.API.Data;

public class TransactionsContextDb
{
    private IMongoDatabase _database { get; }

    public IMongoCollection<Transaction> Transactions
    {
        get => _database.GetCollection<Transaction>("transactions");
    }

    public TransactionsContextDb(IConfiguration configuration)
    {
        try
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
            var client = new MongoClient(settings);
            _database = client.GetDatabase(configuration["DataBase"]);
            MapClasses();
        }
        catch (Exception ex)
        {
            throw new MongoException("It was not possible to connect to MongoDB", ex);
        }
    }

    private void MapClasses()
    {
        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, t => true);

        if (!BsonClassMap.IsClassMapRegistered(typeof(Transaction)))
        {
            BsonClassMap.RegisterClassMap<Transaction>(i =>
            {
                i.AutoMap();
                i.SetIgnoreExtraElements(true);
            });
        }
    }
}