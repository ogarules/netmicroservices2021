using System.Threading.Tasks;
using LibraryApi.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using LipraryApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Moq;

namespace LibraryTests
{
    public class TestStartup
    {
           public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(typeof(Startup).Assembly));

            services.AddDbContext<LibraryDataContext>(r => r.UseInMemoryDatabase("TestDBInMemory"));

            services.AddScoped<ILibraryService, LibraryService>();

            var botServiceMock = new Mock<IBotService>();
            botServiceMock.Setup(r => r.GetBotMessage()).Returns(Task.FromResult("Hola desde el mock"));

            services.AddSingleton<IBotService>(botServiceMock.Object);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}