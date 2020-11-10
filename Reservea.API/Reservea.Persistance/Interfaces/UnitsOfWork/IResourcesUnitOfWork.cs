using Microsoft.EntityFrameworkCore.Storage;
using Reservea.Persistance.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Interfaces.UnitsOfWork
{
    public interface IResourcesUnitOfWork
    {
        IResourceAttributesRepository ResourceAttributesRepository { get; }
        IResourcesRepository ResourcesRepository { get; }
        public IAttributesRepository AttributesRepository { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
        Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
    }
}
