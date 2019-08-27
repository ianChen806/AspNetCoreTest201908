using System.Threading.Tasks;
using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTest201908.Api.Lab06_Service
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab06Controller : Controller
    {
        private readonly IHttpService _httpService;

        public Lab06Controller(IHttpService httpService)
        {
            _httpService = httpService;
        }

        [HttpGet]
        public async Task<IActionResult> Index1()
        {
            var result = await _httpService.IsAuthAsync();
            return Ok(new AuthResult {IsAuth = result});
        }
    }
}