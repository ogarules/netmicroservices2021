using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.Placeholder;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                            .ConfigureAppConfiguration((buildetContext, config) => 
                {
                    config.SetBasePath(buildetContext.HostingEnvironment.ContentRootPath)
                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{buildetContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                          .AddJsonFile("ocelot.json")
                          .AddEnvironmentVariables();
                })
                .AddPlaceholderResolver()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();//.UseUrls("http://*:5006", "https://*:5007");
                });
    }
}
