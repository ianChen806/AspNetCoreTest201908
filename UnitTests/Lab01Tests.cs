using AspNetCoreTest201908.Api.Lab01_ILogger;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace UnitTests
{
    public class Lab01Tests
    {
        [Fact]
        public void Test()
        {
            var logger = new NullLogger<Lab01Controller>();
            var controller = new Lab01Controller(logger);
            var actual = controller.Index1() as OkObjectResult;

            actual.Value.As<AuthResult>().IsAuth.Should().BeTrue();
            actual.Value.As<AuthResult>().Should().BeEquivalentTo(new AuthResult()
            {
                IsAuth = true
            });
        }
    }
}