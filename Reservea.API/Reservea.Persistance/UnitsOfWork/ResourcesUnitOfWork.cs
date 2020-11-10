﻿using AutoMapper;
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
                if (_resourceAttributesRepository == null)
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

        public IAttributesRepository AttributesRepository
        {
            get
            {
                if (_attributesRepository == null)
                {
                    _attributesRepository = new AttributesRepository(_context, _mapper);
                }
                return _attributesRepository;
            }
        }

        private IResourcesRepository _resourcesRepository;
        private IResourceAttributesRepository _resourceAttributesRepository;
        private IAttributesRepository _attributesRepository;

        public ResourcesUnitOfWork(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
