using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTest201908.Api.Lab04_HttpContext;
using AspNetCoreTest201908.Api.Lab05_DbContext;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests
{
    public class Lab05Tests
    {
        [Fact]
        public async Task Add()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(options);
            var list = new List<Profile>()
            {
                new Profile()
            };
            dbContext.Profile.AddRange(list);
            dbContext.SaveChanges();

            var controller = new Lab05Controller(dbContext);

            var actual = await controller.Index1();
            var result = actual as OkObjectResult;

            result.Value.As<List<Profile>>().Should().BeEquivalentTo(list);
        }

        [Fact]
        public async Task Insert()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(options);

            var controller = new Lab05Controller(dbContext);

            await controller.Index2(new ProfileDto()
            {
                Name = "Test"
            });

            var profile = await dbContext.Profile.FirstAsync();
            profile.Name.Should().Be("Test");
        }

        [Fact]
        public async Task Sqlite()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;
            var dbContext = new AppDbContext(options);
            
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
            
            var list = new List<Profile>()
            {
                new Profile()
            };
            dbContext.Profile.AddRange(list);
            dbContext.SaveChanges();
            
            var controller = new Lab05Controller(dbContext);
            var result = await controller.Index1() as OkObjectResult;

            result.Value.As<List<Profile>>().Should().BeEquivalentTo(list);
            
            dbContext.Database.EnsureDeleted();
            dbContext.Database.CloseConnection();
        }
    }
}