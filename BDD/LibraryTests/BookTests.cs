using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Hosting;
using LibraryApi.Data;
using Microsoft.Extensions.DependencyInjection;
using LibraryApi.Domain;
using System.Net.Http;

namespace LibraryTests
{
    [TestClass]
    public class BookTests
    {
        private IConfiguration configuration;
        private TestServer server;

        [TestInitialize]
        public async Task Init()
        {
            this.configuration = new ConfigurationBuilder()
                                     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                     .AddEnvironmentVariables()
                                     .Build();

            this.server = new TestServer(new WebHostBuilder()
                                             .UseEnvironment("Development")
                                             .UseConfiguration(this.configuration)
                                             .UseStartup<TestStartup>());

            var serviceProvider = server.Services;

            using (var context = serviceProvider.GetService<LibraryDataContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var author = new Author { Name = "El OGA" };
                context.Author.Add(author);

                await context.SaveChangesAsync();
            }
        }

        [TestMethod]
        public async Task TestGetAllAuthors()
        {
            using (var response = await this.server.CreateRequest("/api/authors").GetAsync())
            {
                var content = await response.Content.ReadAsStringAsync();
                Assert.AreEqual(true, response.IsSuccessStatusCode);
            }
        }

        [TestMethod]
        public async Task TestAddAuthor()
        {
            var authorToAdd = new Author
            {
                Name = "El OGA 2"
            };

            using (var response = await this.server.CreateRequest("/api/authors")
                                            .And(r => r.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(authorToAdd), Encoding.UTF8, "application/json"))
                                            .PostAsync())
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Author>(content);
                Assert.AreEqual(true, response.IsSuccessStatusCode);
                Assert.IsTrue(result.Id > 0);
            }
        }

        [TestMethod]
        public async Task TestUpdateAuthor()
        {
            var authorToAdd = new Author
            {
                Name = "El OGA 2"
            };

            using (var response = await this.server.CreateRequest("/api/authors/1")
                                            .And(r => r.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(authorToAdd), Encoding.UTF8, "application/json"))
                                            .SendAsync("PUT"))
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Author>(content);
                Assert.AreEqual(true, response.IsSuccessStatusCode);
                Assert.IsTrue(result.Id > 0);
            }
        }

        [TestMethod]
        public async Task TestDeleteAuthor()
        {
            using (var response = await this.server.CreateRequest("/api/authors/1")
                                            .SendAsync("DELETE"))
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Author>(content);
                Assert.AreEqual(true, response.IsSuccessStatusCode);
            }
        }
    }
}
