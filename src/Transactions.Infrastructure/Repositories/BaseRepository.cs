using MongoDB.Driver;
using Transactions.Business.Entities;
using Transactions.Business.Interfaces.Repositories;
using Transactions.Shared.Helpers;

namespace Transactions.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _entityCollection;

    public BaseRepository(TransactionsContextDb context)
    {
        _entityCollection = context.Database.GetCollection<TEntity>(typeof(TEntity).Name.FirstCharToLowerCase());
    }

    public async Task InsertOneAsync(TEntity entity)
        => await _entityCollection.InsertOneAsync(entity);

    public async Task<bool> Exists(FilterDefinition<TEntity> filterDefinition)
    {
        var result = await _entityCollection.CountDocumentsAsync(filterDefinition);
        return result > 0;
    }
}