using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using Steeltoe.Management.Endpoint;
using Steeltoe.Discovery.Client;
using Steeltoe.Common.Http.Discovery;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Service2.Services;

namespace Service2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDiscoveryClient(Configuration);
            services.AddInfoActuatorServices(Configuration);
            services.AddHealthActuatorServices(Configuration);

            services.AddOpenTelemetryTracing((builder) => builder
                        .AddAspNetCoreInstrumentation()
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(Configuration.GetValue<string>("Spring:Application:Name"), typeof(Startup).Namespace, "1.0.0", true))
                        .AddSource(Configuration.GetValue<string>("Spring:Application:Name") + "-V1")
                        .AddHttpClientInstrumentation()
                        .AddConsoleExporter()
                        .AddZipkinExporter());

            services.Configure<ZipkinExporterOptions>(this.Configuration.GetSection("Zipkin"));

            services.AddHttpClient<ValuesService>(r =>
            {
                r.BaseAddress = "service1".ToServiceName();
            })
            .AddServiceDiscovery()
            .AddRoundRobinLoadBalancer()
            .UseHttpClientMetrics();

            services.AddHttpClient("service1", r =>
            {
                r.BaseAddress = "service1".ToServiceName();
            })
            .AddServiceDiscovery()
            .AddRoundRobinLoadBalancer()
            .AddTypedClient(r => Refit.RestService.For<IValuesSerice>(r))
            .UseHttpClientMetrics();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service2", Version = "v1" });
            });

            services.AddHealthChecks().ForwardToPrometheus(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service2 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHttpMetrics();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapAllActuators();
                endpoints.MapMetrics();
            });

            app.UseDiscoveryClient();
        }
    }
}
