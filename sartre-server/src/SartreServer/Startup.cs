using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SartreServer.Repositories;
using SartreServer.Repositories.SqlRepositories;
using SartreServer.Services;

namespace SartreServer
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private ILoggerFactory LoggerFactory { get; }

        private ILogger Logger { get; }

        public Startup(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            LoggerFactory = loggerFactory;
            Configuration = configuration;
            Logger = LoggerFactory.CreateLogger<Startup>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Set up repositories
            services.AddSingleton<IUserRepository>(new SqlUserRepository(Configuration));

            // Set up services
            services.AddSingleton<UserService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
