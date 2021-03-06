﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.EntitiesConfiguration
{
    class ResourceAttributeConfiguration : IEntityTypeConfiguration<ResourceAttribute>
    {
        public void Configure(EntityTypeBuilder<ResourceAttribute> builder)
        {
            builder.Property(x => x.Value)
                .HasMaxLength(255);
            builder.HasKey(x => new { x.AttributeId,x.ResourceId });
        }
    }
}
