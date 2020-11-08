using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Resources.Models;

namespace Reservea.Persistance.Resources.Configuration
{
    class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(63);
            builder.Property(x => x.Description)
                .HasMaxLength(511);
        }
    }
}
