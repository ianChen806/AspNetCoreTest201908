using System;
using System.Threading.Tasks;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTest201908.Api.Lab05_DbContext
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab05Controller : Controller
    {
        private readonly AppDbContext _dbContext;

        public Lab05Controller(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index1()
        {
            return Ok(await _dbContext.Profile.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Index3()
        {
            return Ok(await _dbContext.VProfile.ToListAsync());
        }
        
        [HttpPost]
        public async Task<IActionResult> Index2([FromBody] ProfileDto profileDto)
        {
            var profile = new Profile
            {
                Id = Guid.NewGuid(),
                Name = profileDto.Name
            };
            _dbContext.Profile.Add(profile);

            await _dbContext.SaveChangesAsync();
            
            return Ok(profile);
        }
    }
}