using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Management.Endpoint;
using OpenTelemetry.Logs;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Extensions.Configuration.Placeholder;

namespace Department
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((builderContext, config) =>
            {
                config.SetBasePath(builderContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{builderContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            })
             .AddInfoActuator()
            .AddHealthActuator()
                            .ConfigureLogging((context, builder) => 
                {
                    builder.ClearProviders();
                    builder.AddConsole();
 
                        builder.AddOpenTelemetry(options =>
                        {
                            options.IncludeFormattedMessage = true;
                            options.IncludeScopes = true;
                            options.ParseStateValues = true;
                            
                            options.AddConsoleExporter();
                        });
                })
                .AddPlaceholderResolver()
//                .AddServiceDiscovery(options => options.UseEureka())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();//.UseUrls("http://*:5000");
                });
    }
}
