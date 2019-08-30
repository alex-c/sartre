using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SartreServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Build and run web host
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // Get/set environment info
                    IHostingEnvironment environment = hostingContext.HostingEnvironment;
                    config.SetBasePath(Directory.GetCurrentDirectory());

                    // Add appsettings.json
                    config.AddJsonFile("appsettings.json", optional: false)
                          .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true);
                });
    }
}
