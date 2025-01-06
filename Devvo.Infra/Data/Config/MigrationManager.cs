using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Devvo.Infra.Data.Config
{
    public static class MigrationManager
    {
        public static void MigrateDatabase(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
