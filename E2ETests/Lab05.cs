using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace E2ETests
{
    public class Lab05 : TestBase
    {
        public Lab05(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test()
        {
            var client = CreateClient(collection =>
            {
                collection.AddScoped<IHttpService, FakeHttpClient>();
            });

            var profile = new List<Profile>() {(new Profile() {Id = Guid.NewGuid(), Name = "123"})};
            DbOperator((db) =>
            {
                db.Profile.AddRange(profile);
                db.SaveChanges();
            });

            var message = await client.GetAsync("/api/Lab05/Index1");
            var result = await message.Content.ReadAsAsync<List<Profile>>();
            result.Should().BeEquivalentTo(profile);
        }

        [Fact]
        public async Task Test3()
        {
            var client = CreateClient(collection =>
            {
                collection.AddScoped<IHttpService, FakeHttpClient>();
            });

            var profile = new List<Profile>() {(new Profile() {Id = Guid.NewGuid(), Name = "123"})};
            DbOperator((db) =>
            {
                db.Profile.AddRange(profile);
                db.SaveChanges();
            });

            var message = await client.GetAsync("/api/Lab05/Index3");
            var result = await message.Content.ReadAsAsync<List<VProfile>>();
            result .Should()
                .BeEquivalentTo( profile.Select(r => new VProfile()
                {
                    Name = r.Name
                }));
        }

        [Fact]
        public async Task Test2()
        {
            var client = CreateClient(collection =>
            {
                collection.AddScoped<IHttpService, FakeHttpClient>();
            });
            var profile = new ProfileDto()
            {
                Name = "Test"
            };

            await client.PostAsJsonAsync("/api/Lab05/Index2", profile);

            DbOperator((db) => { db.Profile.First().Name.Should().Be(profile.Name); });
        }
    }
}