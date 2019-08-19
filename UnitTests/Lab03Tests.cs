using AspNetCoreTest201908.Api.Lab02_IConfiguration;
using AspNetCoreTest201908.Api.Lab03_IHostEnvironment;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Xunit;

namespace UnitTests
{
    public class Lab03Tests
    {
        [Fact]
        public void Prod()
        {
            IHostingEnvironment env = new HostingEnvironment();
            env.EnvironmentName = "Production";

            var controller = new Lab03Controller(env);

            var actual = controller.Index1() as OkObjectResult;

            actual.Value.As<EnvResult>().Env.Should().Be("Dev");
        }

        [Fact]
        public void Dev()
        {
            IHostingEnvironment env = new HostingEnvironment();
            env.EnvironmentName = "Dev";

            var controller = new Lab03Controller(env);

            var actual = controller.Index1() as OkObjectResult;

            actual.Value.As<EnvResult>().Env.Should().Be("Prod");
        }
    }
}