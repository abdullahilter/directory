using entity.common;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace service.common;

public static class MongoDbExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, MongoDbOptions mongoDbOptions)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));

        services.AddSingleton(serviceProvider =>
        {
            //var configuration = serviceProvider.GetService<IConfiguration>();
            //var mongoDbOptions = configuration.GetSection(nameof(MongoDbOptions)).Get<MongoDbOptions>();
            var mongoClient = new MongoClient(mongoDbOptions.ConnectionString);
            return mongoClient.GetDatabase(mongoDbOptions.DatabaseName);
        });

        return services;
    }

    public static IServiceCollection AddMongoDbRepository<T>(
        this IServiceCollection services) where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(serviceProvider =>
        {
            var mongoDatabase = serviceProvider.GetService<IMongoDatabase>()!;
            return new MongoDbRepository<T>(mongoDatabase, typeof(T).Name);
        });

        return services;
    }
}