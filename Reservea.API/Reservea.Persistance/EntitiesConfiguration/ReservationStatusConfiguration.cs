using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.EntitiesConfiguration
{
    class ReservationStatusConfiguration : IEntityTypeConfiguration<ReservationStatus>
    {
        public void Configure(EntityTypeBuilder<ReservationStatus> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(63);
        }
    }
}