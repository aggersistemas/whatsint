using MongoDB.Bson.Serialization.Attributes;

namespace WhatsInt.Infrastructure.Entities.Generic
{
    public interface IEntity<TKey>
    {
        [BsonId]
        TKey Id { get; set; }
    }

    public interface IEntity : IEntity<string>
    {
    }
}