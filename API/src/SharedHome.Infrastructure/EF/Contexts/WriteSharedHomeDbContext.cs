using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Infrastructure.EF.Configurations.Write;
using SharedHome.Notifications.Entities;
using SharedHome.Shared.Abstractions.Domain;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Abstractions.User;
using System.Reflection;

namespace SharedHome.Infrastructure.EF.Contexts
{
    public class WriteSharedHomeDbContext : DbContext
    {
        private readonly ITimeProvider _time;

        private readonly ICurrentUser _currentUser;

        public DbSet<Invitation> Invitations { get; set; } = default!;

        public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;

        public DbSet<Bill> Bills { get; set; } = default!;

        public DbSet<Person> Persons { get; set; } = default!;

        public DbSet<HouseGroup> HouseGroups { get; set; } = default!;

        public DbSet<AppNotification> Notifications { get; set; } = default!;

        public DbSet<AppNotificationField> NotificationsFields { get; set; } = default!;


        public WriteSharedHomeDbContext(DbContextOptions<WriteSharedHomeDbContext> options, ITimeProvider time, ICurrentUser currentUser) : base(options)
        {
            _time = time;
            _currentUser = currentUser;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace == typeof(ShoppingListWriteConfiguration).Namespace);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<IAuditable> entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _time.CurrentDate();
                        entry.Entity.CreatedBy = string.IsNullOrEmpty(_currentUser.FirstName) ? entry.Entity.CreatedBy : $"{_currentUser.FirstName} {_currentUser.LastName}";
                        entry.Entity.ModifiedAt = _time.CurrentDate();
                        entry.Entity.ModifiedBy = string.IsNullOrEmpty(_currentUser.FirstName) ? entry.Entity.ModifiedBy : $"{_currentUser.FirstName} {_currentUser.LastName}";
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = _time.CurrentDate();
                        entry.Entity.ModifiedBy = string.IsNullOrEmpty(_currentUser.FirstName) ? entry.Entity.ModifiedBy : $"{_currentUser.FirstName} {_currentUser.LastName}";
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }  
    }
}
