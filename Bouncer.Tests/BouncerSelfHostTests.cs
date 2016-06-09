using Bouncer.Tests.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bouncer.Tests
{
    public class BouncerSelfHostTests
    {
        public BouncerSelfHostTests()
        {
        }

        [Fact]
        public async Task NominalTest()
        {
            using (var host = new OwinSelfHost())
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var response = await client.GetAsync(host.BaseAddress))
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        Assert.True(response.IsSuccessStatusCode);
                        Assert.Contains("Your OWIN application has been successfully started", content);
                    }
                }
            }
        }
    }
}
