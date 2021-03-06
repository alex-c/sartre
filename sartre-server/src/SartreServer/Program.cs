﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System.IO;

namespace SartreServer
{
    public class Program
    {
        /// <summary>
        /// Main program entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Set up Serilog logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            // Build and run web host
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Configures and provides the web host builder.
        /// </summary>
        /// <param name="args">Command line arguments to pass through.</param>
        /// <returns>Returns the configured web host builder.</returns>
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
                })
                .ConfigureLogging(lb => lb.AddSerilog())
                .UseSerilog();
    }
}
