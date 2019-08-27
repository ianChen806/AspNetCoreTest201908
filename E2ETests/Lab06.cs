using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace E2ETests
{
    public class Lab06 : TestBase
    {
        public Lab06(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test()
        {
            var client = CreateClient(collection => { collection.AddScoped<IHttpService, FakeHttpClient>(); });

            var message = await client.GetAsync("/api/Lab06/Index1");
            var result = await message.Content.ReadAsAsync<AuthResult>();
            result.IsAuth.Should().BeTrue();
        }

        [Fact]
        public async Task Test2()
        {
            var client = CreateClient(collection => { collection.AddScoped<IHttpService, FakeHttpClient2>(); });

            var message = await client.GetAsync("/api/Lab06/Index1");
            var result = await message.Content.ReadAsAsync<AuthResult>();
            result.IsAuth.Should().BeFalse();
        }

        [Fact]
        public void Test3()
        {
            CreateClient();
            
            Operator<IHttpService>(async service =>
            {
                var result = await service.IsAuthAsync();
                result.Should().BeTrue();
            });
        }

        [Fact]
        public async Task Test4()
        {
            var provider = new ServiceCollection()
                .AddHttpClient()
                .AddScoped<IHttpService,HttpService>()
                .BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHttpService>();
                var result = await service.IsAuthAsync();
                result.Should().BeTrue();
            }
        }
    }

    public class FakeHttpClient : IHttpService
    {
        public Task<bool> IsAuthAsync()
        {
            return Task.FromResult(true);
        }
    }
}