using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SartreServer.Repositories;
using SartreServer.Repositories.MockRepositories;
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
            if (Configuration.GetValue<bool>("MockData"))
            {
                MockDataProvider dataProvider = new MockDataProvider();
                services.AddSingleton<IUserRepository>(new MockUserRepository(dataProvider));
                services.AddSingleton<IBlogRepository>(new MockBlogRepository(dataProvider));
                services.AddSingleton<IPostRepository>(new MockPostRepository(dataProvider));
            }
            else
            {
                services.AddSingleton<IUserRepository>(new SqlUserRepository(Configuration));
                services.AddSingleton<IBlogRepository>(new SqlBlogRepository(Configuration));
                services.AddSingleton<IPostRepository>(new SqlPostRepository(Configuration));
            }

            // Set up services
            services.AddSingleton<UserService>();
            services.AddSingleton<BlogService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
