using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharedHome.Application.Common.User;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Common.Models;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.Invitations;
using SharedHome.Domain.Persons;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Infrastructure.EF.Configurations.Write;
using SharedHome.Notifications.Entities;
using SharedHome.Shared.Time;
using System.Reflection;

namespace SharedHome.Infrastructure.EF.Contexts
{
    public class WriteSharedHomeDbContext : DbContext
    {
        public DbSet<Invitation> Invitations { get; set; } = default!;

        public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;

        public DbSet<Bill> Bills { get; set; } = default!;

        public DbSet<Person> Persons { get; set; } = default!;

        public DbSet<HouseGroup> HouseGroups { get; set; } = default!;

        public DbSet<AppNotification> Notifications { get; set; } = default!;

        public DbSet<AppNotificationField> NotificationsFields { get; set; } = default!;

        public WriteSharedHomeDbContext(DbContextOptions<WriteSharedHomeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace == typeof(ShoppingListWriteConfiguration).Namespace);
        }      
    }
}
