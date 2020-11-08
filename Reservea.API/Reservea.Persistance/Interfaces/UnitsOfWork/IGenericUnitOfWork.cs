using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Interfaces.UnitsOfWork
{
    public interface IGenericUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
        Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
    }
}
