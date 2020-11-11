using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken);
        Task<TResult> GetByIdAsync<TId, TResult>(TId id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetByListOfIdsAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken);
        Task<IEnumerable<TResult>> GetByListOfIdsAsync<TId, TResult>(IEnumerable<TId> ids, CancellationToken cancellationToken);
        void Add(TEntity resource);
        void AddRange(IEnumerable<TEntity> resource);
        void Remove(TEntity entity);
        Task RemoveByIdAsync<TId>(TId id, CancellationToken cancellationToken);
        void RemoveRange(IEnumerable<TEntity> entity);
        Task RemoveByListOfIdsAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken);
    }
}
