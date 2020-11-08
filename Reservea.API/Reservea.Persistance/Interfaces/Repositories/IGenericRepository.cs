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
        void Add(TEntity resource);
        void AddRange(IEnumerable<TEntity> resource);
    }
}
