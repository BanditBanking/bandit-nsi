using Bandit.NSI.DecisNpgsqlRepository;
using Bandit.NSI.NpgsqlRepository;
using Bandit.NSI.TempNpgsqlRepository;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NSI.Daemon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            EnsureEfMigration(host);

            await host.RunAsync().ConfigureAwait(false);
        }

        // Due to an EF bug, need to use an host builder: https://stackoverflow.com/questions/55970148/apply-entity-framework-migrations-when-using-asp-net-core-in-a-docker-image
        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder().ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            }).UseStartup<Startup>();

            return builder;
        }

        private static void EnsureEfMigration(IWebHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var authContext = services.GetRequiredService<AuthNpgsqlDbContext>();
            if (authContext.Database.GetPendingMigrations().Any())
                authContext.Database.Migrate();

            var tempContext = services.GetRequiredService<TempNpgsqlDbContext>();
            if (tempContext.Database.GetPendingMigrations().Any())
                tempContext.Database.Migrate();

            var decisContext = services.GetRequiredService<DecisNpgsqlDbContext>();
            if (decisContext.Database.GetPendingMigrations().Any())
                decisContext.Database.Migrate();
        }
    }
}
