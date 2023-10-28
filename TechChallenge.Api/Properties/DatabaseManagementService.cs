using Microsoft.EntityFrameworkCore;
using TechChallenge.Infrastructure.Context;

namespace TechChallenge.Api.Properties
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SqlServerContext>();
                context.Database.Migrate();
            }
        }
    }
}
