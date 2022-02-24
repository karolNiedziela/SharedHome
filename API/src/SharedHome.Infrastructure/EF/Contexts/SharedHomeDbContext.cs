using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Infrastructure.Identity.Entities;
using System.Reflection;

namespace SharedHome.Infrastructure.EF.Contexts
{
    public class SharedHomeDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Invitation> Invitations { get; set; } = default!;

        public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;

        public DbSet<Bill> Bills { get; set; } = default!; 

        public DbSet<HouseGroup> HouseGroups { get; set; } = default!;
        
        public SharedHomeDbContext(DbContextOptions<SharedHomeDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
    }
}
