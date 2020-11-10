using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.UnitsOfWork
{
    public abstract class BasicUnitOfWork : IBasicUnitOfWork
    {
        protected DataContext _context;
        protected IMapper _mapper;

        public BasicUnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            await transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }
}
