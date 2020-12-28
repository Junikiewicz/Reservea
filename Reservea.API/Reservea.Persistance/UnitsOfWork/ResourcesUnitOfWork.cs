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
                if(_resourceAttributesRepository is null)
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
                if (_resourcesRepository is null)
                {
                    _resourcesRepository = new ResourcesRepository(_context, _mapper);
                }

                return _resourcesRepository;
            }
        }

        public IAttributesRepository AttributesRepository
        {
            get
            {
                if (_attributesRepository is null)
                {
                    _attributesRepository = new AttributesRepository(_context, _mapper);
                }

                return _attributesRepository;
            }
        }

        public IResourceTypesRepository ResourceTypesRepository
        {
            get
            {
                if (_resourceTypesRepository is null)
                {
                    _resourceTypesRepository = new ResourceTypesRepository(_context, _mapper);
                }

                return _resourceTypesRepository;
            }
        }

        public IResourceTypeAttributesRepository ResourceTypeAttributesRepository
        {
            get
            {
                if (_resourceTypeAttributesRepository is null)
                {
                    _resourceTypeAttributesRepository = new ResourceTypeAttributesRepository(_context, _mapper);
                }

                return _resourceTypeAttributesRepository;
            }
        }

        public IResourceAvailabilitiesRepository ResourceAvailabilitiesRepository
        {
            get
            {
                if (_resourceAvailabilitiesRepository is null)
                {
                    _resourceAvailabilitiesRepository = new ResourceAvailabilitiesRepository(_context, _mapper);
                }

                return _resourceAvailabilitiesRepository;
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
