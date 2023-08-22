using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Transactions;
using Transactions.Shared.Environment;

namespace Transactions.Infrastructure;

public class TransactionsContextDb
{
    public IMongoDatabase Database { get; }

    public TransactionsContextDb()
    {
        try
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(Settings.ConnectionString));
            var client = new MongoClient(settings);
            Database = client.GetDatabase(Settings.Database);
            MapClasses();
        }
        catch (Exception ex)
        {
            throw new MongoException("It was not possible to connect to MongoDB", ex);
        }
    }

    private static void MapClasses()
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