using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reservea.Persistance.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DataContext _context;
        protected IMapper _mapper;

        public GenericRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(CancellationToken cancellationToken) where TResult : class
        {
            var query = _mapper.ProjectTo<TResult>(_context.Set<TEntity>());
            return await query.ToListAsync(cancellationToken);
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            _context.AddRange(entity);
        }
    }
}
