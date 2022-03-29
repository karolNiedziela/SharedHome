using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Shared.Abstractions.Domain;
using SharedHome.Shared.Abstractions.Time;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SharedHome.Infrastructure.EF.Contexts
{
    public class SharedHomeDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ITime _time;

        public DbSet<Invitation> Invitations { get; set; } = default!;

        public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;

        public DbSet<Bill> Bills { get; set; } = default!; 

        public DbSet<HouseGroup> HouseGroups { get; set; } = default!;
        
        public SharedHomeDbContext(DbContextOptions<SharedHomeDbContext> options, ITime time) : base(options)
        {
            _time = time;
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _time.CurrentDate();
                        entry.Entity.ModifiedAt = _time.CurrentDate();
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = _time.CurrentDate();
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        } 
    }
}
