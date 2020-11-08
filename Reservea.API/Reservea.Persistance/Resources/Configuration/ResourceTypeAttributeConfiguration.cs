using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Resources.Models;

namespace Reservea.Persistance.Resources.Configuration
{
    class ResourceTypeAttributeConfiguration : IEntityTypeConfiguration<ResourceTypeAttribute>
    {
        public void Configure(EntityTypeBuilder<ResourceTypeAttribute> builder)
        {
            builder.HasKey(x => new { x.AttributeId, x.ResourceTypeId });
        }
    }
}
