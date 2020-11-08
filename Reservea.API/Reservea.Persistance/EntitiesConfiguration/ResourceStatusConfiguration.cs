using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.EntitiesConfiguration
{
    class ResourceStatusConfiguration : IEntityTypeConfiguration<ResourceStatus>
    {
        public void Configure(EntityTypeBuilder<ResourceStatus> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(63);
        }
    }
}
