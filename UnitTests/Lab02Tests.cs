using System.Collections.Generic;
using AspNetCoreTest201908.Api.Lab02_IConfiguration;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace UnitTests
{
    public class Lab02Tests
    {
        [Fact]
        public void Test()
        {
            var pairs = new Dictionary<string, string>()
            {
                {"Server:Host", "127.0.0.1"}
            };
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(pairs)
                .Build();

            var controller = new Lab021Controller(config);
            var actual = controller.Index1() as OkObjectResult;

            actual.Value.As<ServerResult>().Should().BeEquivalentTo(new ServerResult()
            {
                Host = "127.0.0.1"
            });
        }

        [Fact]
        public void Test1()
        {
            var pairs = new Dictionary<string, string>()
            {
                {"Server:Host", "127.0.0.1"}
            };
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(pairs)
                .Build();

            var controller = new Lab021Controller(config);
            var actual = controller.Index2() as OkObjectResult;

            actual.Value.As<ServerResult>().Should().BeEquivalentTo(new ServerResult()
            {
                Host = "127.0.0.1"
            });
        }

        [Fact]
        public void Test2()
        {
            var options = new OptionsWrapper<ServerHost>(new ServerHost()
            {
                Host = "127.0.0.1"
            });
            var controller = new Lab022Controller(options);

            var actual = controller.Index1() as OkObjectResult;

            actual.Value.As<ServerResult>().Should().BeEquivalentTo(new ServerResult()
            {
                Host = "127.0.0.1"
            });
        }
    }
}