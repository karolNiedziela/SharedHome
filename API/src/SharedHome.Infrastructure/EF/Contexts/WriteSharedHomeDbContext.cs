using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Infrastructure.EF.Configurations.Write;
using SharedHome.Infrastructure.EF.Extensions;
using SharedHome.Infrastructure.EF.Lookups;
using SharedHome.Shared.Abstractions.Domain;
using SharedHome.Shared.Abstractions.Time;
using System.Reflection;

namespace SharedHome.Infrastructure.EF.Contexts
{
    public class WriteSharedHomeDbContext : DbContext
    {
        private readonly ITimeProvider _time;

        public DbSet<Invitation> Invitations { get; set; } = default!;

        public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;

        public DbSet<Bill> Bills { get; set; } = default!;

        public DbSet<Person> Persons { get; set; } = default!;

        public DbSet<HouseGroup> HouseGroups { get; set; } = default!;

   
        public WriteSharedHomeDbContext(DbContextOptions<WriteSharedHomeDbContext> options, ITimeProvider time) : base(options)
        {
            _time = time;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace == typeof(ShoppingListWriteConfiguration).Namespace);
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
