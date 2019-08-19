using System;
using AspNetCoreTest201908.Api.Lab00;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class Lab00Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Lab00Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test()
        {
            var controller = new Lab00Controller();
            var actual = controller.Index() as OkObjectResult;

            actual.Value.As<AuthResult>().IsAuth.Should().BeTrue();
            actual.Value.As<AuthResult>().Should().BeEquivalentTo(new AuthResult()
            {
                IsAuth = true
            });
        }
    }
}