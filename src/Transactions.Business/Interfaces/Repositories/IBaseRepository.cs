using MongoDB.Driver;
using Transactions.Business.Entities;

namespace Transactions.Business.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task InsertOneAsync(TEntity entity);

    public Task<bool> Exists(FilterDefinition<TEntity> filterDefinition);
}