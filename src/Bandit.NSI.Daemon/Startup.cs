using Bandit.NSI.AuthNpgsqlRepository.Repositories;
using Bandit.NSI.Daemon.Configuration;
using Bandit.NSI.Daemon.Extensions;
using Bandit.NSI.Daemon.Services;
using Bandit.NSI.DecisNpgsqlRepository;
using Bandit.NSI.DecisNpgsqlRepository.Repositories;
using Bandit.NSI.NpgsqlRepository;
using Bandit.NSI.TempNpgsqlRepository;
using Bandit.NSI.TempNpgsqlRepository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NSI.Daemon
{
    public class Startup
    {
        private IConfiguration _configuration;
        private DaemonConfiguration _parsedConfiguration;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _parsedConfiguration = configuration.GetSection(DaemonConfiguration.ServiceName).Get<DaemonConfiguration>() ?? new DaemonConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddSingleton(_parsedConfiguration)
                .AddEndpointsApiExplorer()
                .AddCorsHandling()
                .AddJwtAuthentication(_parsedConfiguration.JWT)
                .AddSwaggerService(_parsedConfiguration.API)
                .AddLogging(b =>
                {
                    b.AddConfiguration(_configuration.GetSection("Logging"));
                    b.AddConsole();
                })
                .AddScoped<IAccountsRepository, AccountsRepository>()
                .AddScoped<ITempStudyRepository, TempStudyRepository>()
                .AddScoped<IDecisPublicationRepository, DecisPublicationRepository>()
                .AddScoped<ITokenService, JwtTokenService>()
                .AddScoped<IAccountsService, AccountsService>()
                .AddScoped<IStudyService, StudyService>()
                .AddDbContext<AuthNpgsqlDbContext>(options => options.UseNpgsql(_parsedConfiguration.AuthDatabase.ConnectionString))
                .AddDbContext<TempNpgsqlDbContext>(options => options.UseNpgsql(_parsedConfiguration.TempDatabase.ConnectionString))
                .AddDbContext<DecisNpgsqlDbContext>(options => options.UseNpgsql(_parsedConfiguration.DecisDatabase.ConnectionString));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.DocumentationPath = _parsedConfiguration.API.ErrorDocumentationUri;
            });

            app.UseCors("ALLOW_ANY");

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
