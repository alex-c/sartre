using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SartreServer.Repositories;
using SartreServer.Repositories.MockRepositories;
using SartreServer.Repositories.SqlRepositories;
using SartreServer.Services;
using System.Text;

namespace SartreServer
{
    /// <summary>
    /// Sartre server startup and configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// CORS policy name for local development.
        /// </summary>
        private readonly string LOCAL_DEVELOPMENT_CORS_POLICY = "localDevelopmentCorsPolicy";

        /// <summary>
        /// The hosting environment information.
        /// </summary>
        private IHostingEnvironment Environment { get; }

        /// <summary>
        /// The app configuration.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Logger factory to use locally and pass through to services.
        /// </summary>
        private ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// Local logger instance.
        /// </summary>
        private ILogger Logger { get; }
        
        /// <summary>
        /// Sets up the Startup class with everything needed for configuration.
        /// </summary>
        /// <param name="loggerFactory">Logger factory to create loggers from.</param>
        /// <param name="environment">Hosting environment information.</param>
        /// <param name="configuration">App configuration as set up by the web host builder.</param>
        public Startup(ILoggerFactory loggerFactory, IHostingEnvironment environment, IConfiguration configuration)
        {
            LoggerFactory = loggerFactory;
            Environment = environment;
            Configuration = configuration;
            Logger = LoggerFactory.CreateLogger<Startup>();
        }

        /// <summary>
        /// Configures the available services for dependency injection.
        /// </summary>
        /// <param name="services">Service collection to register services with.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy(LOCAL_DEVELOPMENT_CORS_POLICY, builder =>
                {
                    builder.WithOrigins("http://localhost:8080");
                });
            });

            // Configure JWT-based auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetValue<string>("Jwt:Issuer"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Jwt:Secret")))
                    };
                });

            // Configure MVC
            services.AddMvc();

            // Set up repositories
            if (Configuration.GetValue<bool>("Mocking:UseMockDataPersistence"))
            {
                MockDataProvider dataProvider = null;
                if (Configuration.GetValue<bool>("Mocking:SeedWithMockDataOnStartup"))
                {
                    dataProvider = new MockDataProvider();
                }

                services.AddSingleton<IPlatformConfigurationRepository>(new MockPlatformConfigurationRepository(dataProvider));

                MockUserRepository userRepository = new MockUserRepository(dataProvider);
                services.AddSingleton<IReadOnlyUserRepository>(userRepository);
                services.AddSingleton<IUserRepository>(userRepository);

                MockBlogRepository blogRepository = new MockBlogRepository(dataProvider);
                services.AddSingleton<IReadOnlyBlogRepository>(blogRepository);
                services.AddSingleton<IBlogRepository>(blogRepository);

                services.AddSingleton<IPostRepository>(new MockPostRepository(dataProvider));
            }
            else
            {
                services.AddSingleton<IUserRepository>(new SqlUserRepository(Configuration));
                services.AddSingleton<IBlogRepository>(new SqlBlogRepository(Configuration));
                services.AddSingleton<IPostRepository>(new SqlPostRepository(Configuration));
            }

            // Set up services
            services.AddSingleton<PasswordHashingService>();
            services.AddSingleton<AuthService>();
            services.AddSingleton<PlatformConfigutationService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<BlogService>();
        }

        /// <summary>
        /// Configures the app.
        /// </summary>
        /// <param name="app">Application builder to configure the app through.</param>
        public void Configure(IApplicationBuilder app)
        {
            // Use CORS
            if (Environment.IsDevelopment())
            {
                app.UseCors(LOCAL_DEVELOPMENT_CORS_POLICY);
            }

            // Use JWT-based auth
            app.UseAuthentication();

            // Use MVC
            app.UseMvc();
        }
    }
}
