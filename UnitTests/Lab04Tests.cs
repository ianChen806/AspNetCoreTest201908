using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreTest201908.Api.Lab03_IHostEnvironment;
using AspNetCoreTest201908.Api.Lab04_HttpContext;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTests
{
    public class Lab04Tests
    {
        [Fact]
        public void IsAuth()
        {
            var identity = new GenericIdentity("Email", ClaimTypes.Email);
            var principal = new GenericPrincipal(identity, null);
            var accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = principal
                }
            };

            var controller = new Lab04Controller(accessor);

            var actual = controller.Index1() as OkObjectResult;

            actual.Value.As<AuthResult>().IsAuth.Should().BeTrue();
        }

        [Fact]
        public void Email()
        {
            var accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new List<ClaimsIdentity>()
                    {
                        new ClaimsIdentity(new List<Claim>()
                        {
                            new Claim(ClaimTypes.Email, "Email"),
                        })
                    })
                }
            };

            var controller = new Lab04Controller(accessor);

            var actual = controller.Index2() as OkObjectResult;

            actual.Value.As<AuthClaim>().Email.Should().Be("Email");
        }

        [Fact]
        public void Session()
        {
            var accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
                {
                    Session = new TestSession()
                }
            };

            var controller = new Lab04Controller(accessor);

            var actual = controller.Index3() as OkObjectResult;

            actual.Value.As<AuthUser>().User.Should().Be("user");
        }

        [Fact]
        public void Cookie()
        {
            var accessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext()
                {
                    Request =
                    {
                        Cookies = new RequestCookieCollection(new Dictionary<string, string>()
                        {
                            {"user", "value"}
                        })
                    }
                }
            };

            var controller = new Lab04Controller(accessor);
            var actual = controller.Index4() as OkObjectResult;

            actual.Value.As<AuthUser>().User.Should().Be("value");
        }
    }

    public class TestSession : ISession
    {
        public Task LoadAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            value = Encoding.UTF8.GetBytes(key);
            return true;
        }

        public void Set(string key, byte[] value)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool IsAvailable { get; }
        public string Id { get; }
        public IEnumerable<string> Keys { get; }
    }
}