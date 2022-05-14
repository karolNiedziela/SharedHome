using Microsoft.EntityFrameworkCore;
using SharedHome.Infrastructure.EF.Configurations.Read;
using SharedHome.Infrastructure.EF.Models;
using System.Reflection;

namespace SharedHome.Infrastructure.EF.Contexts
{
    internal class ReadSharedHomeDbContext : DbContext
    {
        public DbSet<ShoppingListReadModel> ShoppingLists { get; set; } = default!;

        public DbSet<ShoppingListProductReadModel> ShoppingListProducts { get; set;} = default!;

        public DbSet<HouseGroupReadModel> HouseGroups { get; set; } = default!;

        public DbSet<HouseGroupMemberReadModel> HouseGroupMembers { get; set; } = default!;

        public DbSet<PersonReadModel> Persons { get; set; } = default!;

        public DbSet<InvitationReadModel> Invitations { get; set; } = default!;

        public DbSet<BillReadModel> Bills { get; set; } = default!;


        public ReadSharedHomeDbContext(DbContextOptions<ReadSharedHomeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace == typeof(ShoppingListReadConfiguration).Namespace);
        }

    }
}
