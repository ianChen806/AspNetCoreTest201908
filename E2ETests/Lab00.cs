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
    public class Lab00 : TestBase
    {
        public Lab00(WebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Test()
        {
            var client = CreateClient(collection =>
            {
                collection.AddScoped<IHttpService, FakeHttpClient>();
            });

            var message = await client.GetAsync("/api/Lab00/Index");
            var result = await message.Content.ReadAsAsync<AuthResult>();

            result.IsAuth.Should().BeTrue();
        }
    }
}