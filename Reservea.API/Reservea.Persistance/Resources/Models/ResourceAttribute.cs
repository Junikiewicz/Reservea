namespace Reservea.Persistance.Resources.Models
{
    public class ResourceAttribute
    {
        public int AttributeId { get; set; }
        public int ResourceId { get; set; }
        public string Value { get; set; }
        
        public virtual Attribute Attribute { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
