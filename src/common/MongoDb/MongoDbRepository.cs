using MongoDB.Driver;
using System.Linq.Expressions;

namespace common.MongoDb;

public class MongoDbRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _collection;
    private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder = Builders<T>.Filter;

    public MongoDbRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        _collection = mongoDatabase.GetCollection<T>(collectionName);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, null, cancellationToken);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        var results = await _collection
            .Find(_filterDefinitionBuilder.Empty)
            .ToListAsync(cancellationToken);

        return results;
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        var results = await _collection.Find(filter).ToListAsync(cancellationToken);

        return results;
    }

    public async Task<T> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = _filterDefinitionBuilder.Eq(x => x.Id, id);
        var result = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        var result = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var filter = _filterDefinitionBuilder.Eq(x => x.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }
}