using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bouncer.SystemWeb
{
    public class MikeModule : IHttpModule
    {
        private static MikeIds _bouncer;

        static MikeModule()
        {
            _bouncer = new MikeIds();
        }

        public static MikeIds BouncerManager
        {
            get { return _bouncer; }
            set { _bouncer = value; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication application)
        {
            EventHandlerTaskAsyncHelper asyncBeginRequest = new EventHandlerTaskAsyncHelper(Application_BeginRequest);
            application.AddOnBeginRequestAsync(asyncBeginRequest.BeginEventHandler, asyncBeginRequest.EndEventHandler);
        }

        private async Task Application_BeginRequest(object sender, EventArgs e)
        {
            var context = ((HttpApplication)sender).Context;
            var request = ((HttpApplication)sender).Request;
            var response = ((HttpApplication)sender).Response;

            var bouncer = _bouncer;
            if (bouncer != null)
            {
                IRequestContext requestcontext = new SystemWebRequest(request);
                var report = bouncer.Analyze(requestcontext);
                if (report.ActionRequired)
                {
                    IPlatform platform = new SystemWebPlatform(context);
                    bool canContinue = await bouncer.TakeActionBasedOnReportAsync(report, platform);
                    if (!canContinue)
                    {
                        if (response.SupportsAsyncFlush)
                        {
                            await Task.Factory.FromAsync(response.BeginFlush, response.EndFlush, null);
                        }
                        else
                        {
                            response.Flush();
                        }

                        response.End();
                    }
                }
            }
        }
    }
}
