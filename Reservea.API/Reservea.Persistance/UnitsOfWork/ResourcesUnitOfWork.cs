using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Repositories;

namespace Reservea.Persistance.UnitsOfWork
{
    public class ResourcesUnitOfWork : BasicUnitOfWork, IResourcesUnitOfWork
    {
        public IResourceAttributesRepository ResourceAttributesRepository
        {
            get
            {
                return _resourceAttributesRepository ?? new ResourceAttributesRepository(_context, _mapper);
            }
        }

        public IResourcesRepository ResourcesRepository
        {
            get
            {
                return _resourcesRepository ?? new ResourcesRepository(_context, _mapper);
            }
        }

        public IAttributesRepository AttributesRepository
        {
            get
            {
                return _attributesRepository ?? new AttributesRepository(_context, _mapper);
            }
        }

        public IResourceTypesRepository ResourceTypesRepository
        {
            get
            {
                return _resourceTypesRepository ?? new ResourceTypesRepository(_context, _mapper);
            }
        }

        public IResourceTypeAttributesRepository ResourceTypeAttributesRepository
        {
            get
            {
                return _resourceTypeAttributesRepository ?? new ResourceTypeAttributesRepository(_context, _mapper);
            }
        }

        public IResourceAvailabilitiesRepository ResourceAvailabilitiesRepository
        {
            get
            {
                return _resourceAvailabilitiesRepository ?? new ResourceAvailabilitiesRepository(_context, _mapper);
            }
        }

        private IResourcesRepository _resourcesRepository;
        private IResourceAttributesRepository _resourceAttributesRepository;
        private IAttributesRepository _attributesRepository;
        private IResourceTypesRepository _resourceTypesRepository;
        private IResourceTypeAttributesRepository _resourceTypeAttributesRepository;
        private IResourceAvailabilitiesRepository _resourceAvailabilitiesRepository;

        public ResourcesUnitOfWork(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
