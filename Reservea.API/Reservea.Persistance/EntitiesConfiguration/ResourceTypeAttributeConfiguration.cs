using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.EntitiesConfiguration
{
    class ResourceTypeAttributeConfiguration : IEntityTypeConfiguration<ResourceTypeAttribute>
    {
        public void Configure(EntityTypeBuilder<ResourceTypeAttribute> builder)
        {
            builder.HasKey(x => new { x.AttributeId, x.ResourceTypeId });
        }
    }
}
