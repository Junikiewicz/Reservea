using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Resources.Models;

namespace Reservea.Persistance.Resources.Configuration
{
    public class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(63);
        }
    }
}
