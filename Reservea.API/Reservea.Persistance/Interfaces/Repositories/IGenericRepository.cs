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
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        Task DeleteByIdAsync<TId>(TId id, CancellationToken cancellationToken);
        void DeleteRange(IEnumerable<TEntity> entity);
        Task DeleteByListOfIdsAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken);
    }
}
