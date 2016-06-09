using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bouncer.SystemWeb
{
    public class BouncerModule : IHttpModule
    {
        private static BouncerManager _bouncer;

        static BouncerModule()
        {
            _bouncer = new BouncerManager();
        }

        public static BouncerManager BouncerManager
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
            application.BeginRequest += Application_BeginRequest;
        }

        private async void Application_BeginRequest(object sender, EventArgs e)
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
