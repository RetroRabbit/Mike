using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouncer.Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    internal class MikeMiddleware
    {
        private MikeIds _bouncer;
        private Func<IDictionary<string, object>, Task> _next;

        public MikeMiddleware(AppFunc next, MikeIds bouncer)
        {
            _bouncer = bouncer;
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var request = new OwinRequestContext(environment);
            var report = _bouncer.Analyze(request);
            if (report.ActionRequired)
            {
                bool mustStillInvokeNext = await _bouncer.TakeActionBasedOnReportAsync(report, new OwinPlatform(environment));
                if(mustStillInvokeNext)
                {
                    await _next(environment);
                }
            }
            else
            {
                await _next(environment);
            }
        }
    }
}
