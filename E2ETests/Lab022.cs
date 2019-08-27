using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace E2ETests
{
    public class Lab022:TestBase
    {
        public Lab022(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test()
        {
            var client = CreateClient();

            var message = await client.GetAsync("/api/Lab022/Index1");
            var result = await message.Content.ReadAsAsync<ServerResult>();

            result.Host.Should().Be("Test");
        }
    }
}