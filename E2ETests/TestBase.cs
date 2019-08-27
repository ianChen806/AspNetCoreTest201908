using System;
using System.Net.Http;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace E2ETests
{
    public class TestBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        protected WebApplicationFactory<Startup> TestWebHost;

        protected TestBase(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateClient(Action<IServiceCollection> serviceConfig = null)
        {
            TestWebHost = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");

                if (serviceConfig != null)
                {
                    builder.ConfigureTestServices(serviceConfig);
                }

                builder.ConfigureServices(collection =>
                {
                    collection.AddDbContext<AppDbContext>(optionsBuilder =>
                    {
                        // optionsBuilder.UseInMemoryDatabase("memory");
                        optionsBuilder.UseSqlite("DataSource=name");
                    });

                    var serviceProvider = collection.BuildServiceProvider();
                    using (var serviceScope = serviceProvider.CreateScope())
                    {
                        var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                        appDbContext.Database.EnsureDeleted();
                        appDbContext.Database.EnsureCreated();

                        appDbContext.Database.ExecuteSqlCommand(
                            @" create view VProfile
                                as
                                select Name from Profile");
                    }
                });
            });
            return TestWebHost.CreateClient();
        }

        protected void DbOperator(Action<AppDbContext> action)
        {
            Operator(action);
        }

        protected void Operator<T>(Action<T> action)
        {
            using (var scope = TestWebHost.Server.Host.Services.CreateScope())
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<T>();
                action(appDbContext);
            }
        }
    }
}