using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedHome.Identity.Entities;

namespace SharedHome.Identity.EF.Contexts
{
    public class IdentitySharedHomeDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserImage> UserImages { get; set; } = default!;

        public IdentitySharedHomeDbContext(DbContextOptions<IdentitySharedHomeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().Navigation(x => x.Images).AutoInclude();
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }

    }
}
