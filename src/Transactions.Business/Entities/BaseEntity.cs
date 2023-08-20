using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Transactions.Business.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = ObjectId.GenerateNewId();
            CreatedDate = DateTime.UtcNow;
        }

        [BsonId]
        public ObjectId Id { get; private set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public FilterDefinition<TEntity> GetByIdDefinition<TEntity>()
            => Builders<TEntity>.Filter.Eq("_id", Id);

        public static FilterDefinition<TEntity> GetByIdDefinition<TEntity>(string id)
            => Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
    }
}