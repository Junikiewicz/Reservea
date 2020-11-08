using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(CancellationToken cancellationToken) where TResult : class;
        void Add(TEntity resource);
        void AddRange(IEnumerable<TEntity> resource);
    }
}
