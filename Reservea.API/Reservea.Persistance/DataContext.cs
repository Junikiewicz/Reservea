using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reservea.Persistance.Models;
using System.Reflection;

namespace Reservea.Persistance
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceAttribute> ResourceAttributes { get; set; }
        public virtual DbSet<ResourceStatus> ResourceStatuses { get; set; }
        public virtual DbSet<ResourceType> ResourceTypes { get; set; }
        public virtual DbSet<ResourceTypeAttribute> ResourceTypeAttributes { get; set; }
        public virtual DbSet<ResourceAvailability> ResourceAvailabilities { get; set; }

        public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
