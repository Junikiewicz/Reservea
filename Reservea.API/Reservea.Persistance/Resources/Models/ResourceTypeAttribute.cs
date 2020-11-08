namespace Reservea.Persistance.Resources.Models
{
    public class ResourceTypeAttribute
    {
        public int AttributeId { get; set; }
        public int ResourceTypeId { get; set; }

        public virtual Attribute Attribute { get; set; }
        public virtual ResourceType ResourceType { get; set; }
    }
}
