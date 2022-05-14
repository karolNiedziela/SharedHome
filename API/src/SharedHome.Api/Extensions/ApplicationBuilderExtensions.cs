using Microsoft.EntityFrameworkCore;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<WriteSharedHomeDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
