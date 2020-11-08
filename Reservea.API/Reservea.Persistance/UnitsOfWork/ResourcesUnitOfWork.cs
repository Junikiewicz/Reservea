using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Repositories;

namespace Reservea.Persistance.UnitsOfWork
{
    public class ResourcesUnitOfWork : GenericUnitOfWork, IResourcesUnitOfWork
    {
        public IResourceAttributesRepository ResourceAttributesRepository
        {
            get
            {
                if (_resourcesRepository == null)
                {
                    _resourceAttributesRepository = new ResourceAttributesRepository(_context, _mapper);
                }
                return _resourceAttributesRepository;
            }
        }

        public IResourcesRepository ResourcesRepository
        {
            get
            {
                if (_resourcesRepository == null)
                {
                    _resourcesRepository = new ResourcesRepository(_context, _mapper);
                }
                return _resourcesRepository;
            }
        }

        private IResourcesRepository _resourcesRepository;
        private IResourceAttributesRepository _resourceAttributesRepository;

        public ResourcesUnitOfWork(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
