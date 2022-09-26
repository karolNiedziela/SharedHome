using Microsoft.EntityFrameworkCore;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Options;
using SharedHome.Shared.Time;
using SharedHome.Shared.User;
using System;

namespace SharedHome.IntegrationTests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public WriteSharedHomeDbContext WriteContext { get; }

        internal ReadSharedHomeDbContext ReadContext { get; }

        public DatabaseFixture()
        {
            var settings = new SettingsProvider().Get<MySQLSettings>(MySQLSettings.SectionName);
 
            WriteContext = new WriteSharedHomeDbContext(new DbContextOptionsBuilder<WriteSharedHomeDbContext>()
                 .UseMySql(settings.ConnectionString, ServerVersion.AutoDetect(settings.ConnectionString)).Options,
                new UtcTimeProvider(),
                null!);

            ReadContext = new ReadSharedHomeDbContext(new DbContextOptionsBuilder<ReadSharedHomeDbContext>()
              .UseMySql(settings.ConnectionString, ServerVersion.AutoDetect(settings.ConnectionString)).Options);

            WriteContext.Database.Migrate();

            Utilities.InitializeDbForTests(WriteContext);
        }

        public void Dispose()
        {
            WriteContext.Database.EnsureDeleted();
            WriteContext.Dispose();
            ReadContext.Database.EnsureDeleted();
            ReadContext.Dispose();
        }
    }
}
