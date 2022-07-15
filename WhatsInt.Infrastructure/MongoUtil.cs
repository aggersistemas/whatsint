using Infrastructure.Entities.Generic;
using Infrastructure.Models;
using MongoDB.Driver;

namespace Infrastructure
{
    internal static class MongoUtil<TU>
    {
        private static IMongoDatabase GetDatabaseFromUrl(MongoUrl mongoUrl, string databaseName)
        {
            IMongoClient mongoClient = new MongoClient(mongoUrl);

            return mongoClient.GetDatabase(databaseName);
        }

        public static IMongoCollection<T> GetCollectionFromSettings<T>(IDatabaseSettings databaseSettings) where T : IEntity<TU>
        {
            return GetCollectionFromSettings<T>(databaseSettings.ConnectionString, GetCollectionName<T>(), databaseSettings.DatabaseName);
        }

        public static IMongoCollection<T> GetCollectionFromSettings<T>(string connectionString, string collectionName, string databaseName) where T : IEntity<TU>
        {
            return GetDatabaseFromUrl(new MongoUrl(connectionString), databaseName).GetCollection<T>(collectionName);
        }

        private static string GetCollectionName<T>() where T : IEntity<TU>
        {
            var baseType = typeof(T).BaseType;

            var collectionName = baseType != null && baseType == typeof(object) ? GetCollectioNameFromInterface<T>() : GetCollectionNameFromType(typeof(T));

            var withoutCollectionName = string.IsNullOrEmpty(collectionName);

            if (withoutCollectionName) throw new ArgumentException("Collection name cannot be empty for this entity.");

            return collectionName;
        }

        private static string GetCollectioNameFromInterface<T>()
        {
            var customAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));

            var collectionName = customAttribute != null ? ((CollectionName)customAttribute).Name : typeof(T).Name;

            return collectionName;
        }

        private static string GetCollectionNameFromType(Type entitytype)
        {
            var customAttribute = Attribute.GetCustomAttribute(entitytype, typeof(CollectionName));

            if (customAttribute != null)
            {
                return ((CollectionName)customAttribute).Name;
            }

            if (typeof(Entity).IsAssignableFrom(entitytype))
            {
                while (entitytype.BaseType != null && entitytype.BaseType != typeof(Entity))
                {
                    entitytype = entitytype.BaseType;
                }
            }

            return entitytype.Name;
        }
    }
}