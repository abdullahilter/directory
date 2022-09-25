using System.Linq.Expressions;

namespace common;

public interface IRepository<T> where T : IEntity
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    Task<T> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<T> GetAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}