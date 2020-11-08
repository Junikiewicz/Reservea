using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Resources.Models;

namespace Reservea.Persistance.Resources.Configuration
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
