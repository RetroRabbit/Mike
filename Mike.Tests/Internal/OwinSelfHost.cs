using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mike.Tests.Internal
{
    using System.Threading;
    using AppFunc = Func<IOwinContext, Func<Task>, Task>;

    public class OwinSelfHost : IDisposable
    {
        private static object _syncroot = new object();
        private static int _port = 21002;
        private IDisposable _runningApi;
        public string BaseAddress { get; set; }


        public OwinSelfHost(params AppFunc[] additionalMiddleware)
        {
            if (_runningApi != null) throw new InvalidOperationException("The API is already started.");

            if(string.IsNullOrEmpty(BaseAddress))
            {
                BaseAddress = $"http://localhost:{Interlocked.Increment(ref _port)}/";
            }

            lock (_syncroot)
            {
                Startup.AdditionalMiddleware.Clear();
                Startup.AdditionalMiddleware.AddRange(additionalMiddleware);
                _runningApi = WebApp.Start<Startup>(BaseAddress);
                Startup.AdditionalMiddleware.Clear();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_runningApi != null)
                {
                    _runningApi.Dispose();
                    _runningApi = null;
                }
            }
        }
    }
}
