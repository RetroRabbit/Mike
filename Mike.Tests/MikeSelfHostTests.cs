using Mike.Tests.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mike.Tests
{
    public class MikeSelfHostTests
    {
        public MikeSelfHostTests()
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

        [Fact]
        public async Task NominalIpAddressTest()
        {
            using (var host = new OwinSelfHost())
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var response = await client.GetAsync($"{host.BaseAddress}ipaddress"))
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        Assert.True(response.IsSuccessStatusCode);
                        Assert.Equal("::1", content);
                    }
                }
            }
        }

        [Fact]
        public async Task IpAddressRewriteTest()
        {
            using (var host = new OwinSelfHost())
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, $"{host.BaseAddress}ipaddress");
                    request.Headers.Add("X-Forwarded-For", "196.32.12.33");

                    using (var response = await client.SendAsync(request))
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        Assert.True(response.IsSuccessStatusCode);
                        Assert.Equal("196.32.12.33", content);
                    }
                }
            }
        }

        [Fact]
        public async Task IpAddressRewriteLocalTest()
        {
            using (var host = new OwinSelfHost())
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, $"{host.BaseAddress}ipaddress");
                    request.Headers.Add("X-Forwarded-For", "172.27.12.33");

                    using (var response = await client.SendAsync(request))
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        Assert.True(response.IsSuccessStatusCode);
                        Assert.Equal("::1", content);
                    }
                }
            }
        }
    }
}
