using System.Threading.Tasks;
using AspNetCoreTest201908.Model;

namespace E2ETests
{
    public class FakeHttpClient2 : IHttpService
    {
        public Task<bool> IsAuthAsync()
        {
            return Task.FromResult(false);
        }
    }
}