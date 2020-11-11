using Reservea.Persistance.Interfaces.Repositories;

namespace Reservea.Persistance.Interfaces.UnitsOfWork
{
    public interface IResourcesUnitOfWork : IBasicUnitOfWork
    {
        IResourceAttributesRepository ResourceAttributesRepository { get; }
        IResourcesRepository ResourcesRepository { get; }
        IAttributesRepository AttributesRepository { get; }
        IResourceTypesRepository ResourceTypesRepository { get; }
        IResourceTypeAttributesRepository ResourceTypeAttributesRepository { get; }
    }
}
