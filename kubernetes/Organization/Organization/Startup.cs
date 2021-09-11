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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Polly;
using Organization.Data;

using Microsoft.EntityFrameworkCore;
using Organization.Services;

namespace Organization
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

services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("Authorization");
            });

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

            services.AddDbContext<OrganizationContext>(r => r.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"), sqlServerOptionsAction: sqloptions =>
            {
                sqloptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
            }));

            services.AddHttpClient<DepartmentService>(r =>
            {
                r.BaseAddress = "department".ToServiceName();
            })
            .AddMicroserviceOptions();

            services.AddHttpClient<EmployeeService>(r =>
            {
                r.BaseAddress = "employee".ToServiceName();
            })
            .AddMicroserviceOptions();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Organization", Version = "v1" });
            });

            services.AddScoped<Services.OrganizationService>();

            services.AddHealthChecks().ForwardToPrometheus();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = Configuration.GetValue<string>("authority");
                        options.Audience = Configuration.GetValue<string>("audience");
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(r =>
            {
                r.AllowAnyOrigin();
                r.AllowAnyMethod();
                r.AllowAnyHeader();
            });

            app.UseHeaderPropagation();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DepartmentService v1"));
            }

      //      app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHttpMetrics();

            app.UseAuthentication();
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
