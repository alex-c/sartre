using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SartreServer
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private ILoggerFactory LoggerFactory { get; }

        public Startup(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            LoggerFactory = loggerFactory;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
