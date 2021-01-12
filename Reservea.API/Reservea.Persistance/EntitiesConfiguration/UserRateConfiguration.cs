using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.EntitiesConfiguration
{
    class UserRateConfiguration : IEntityTypeConfiguration<UserRate>
    {
        public void Configure(EntityTypeBuilder<UserRate> builder)
        {
            builder.Property(x => x.Feedback)
                 .HasMaxLength(2047);
        }
    }
}
