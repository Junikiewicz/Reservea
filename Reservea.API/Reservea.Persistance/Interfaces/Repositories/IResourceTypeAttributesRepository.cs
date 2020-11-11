using Reservea.Persistance.Models;

namespace Reservea.Persistance.Interfaces.Repositories
{
    public interface IResourceTypeAttributesRepository : IGenericRepository<ResourceTypeAttribute>
    {
       
    }

    public class ResourceTypeAttributePrimaryKey
    {
        public int ResourceTypeId { get; set; }
        public int AttributeId { get; set; }
    }
}
