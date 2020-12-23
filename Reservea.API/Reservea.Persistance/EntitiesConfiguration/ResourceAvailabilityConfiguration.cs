using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.EntitiesConfiguration
{
    class ResourceAvailabilityConfiguration : IEntityTypeConfiguration<ResourceAvailability>
    {
        public void Configure(EntityTypeBuilder<ResourceAvailability> builder)
        {

        }
    }
}
