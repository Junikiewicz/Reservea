using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.EntitiesConfiguration
{
    public class TextFieldContentConfiguration : IEntityTypeConfiguration<TextFieldContent>
    {
        public void Configure(EntityTypeBuilder<TextFieldContent> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(255);

            builder.Property(x => x.Content)
                .HasMaxLength(4080);
        }
    }
}
