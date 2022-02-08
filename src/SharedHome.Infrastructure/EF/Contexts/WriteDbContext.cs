using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.ShoppingLists.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.EF.Contexts
{
    internal class WriteDbContext : DbContext
    {
        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;
        
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
