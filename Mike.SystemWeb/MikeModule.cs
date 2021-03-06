﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mike.SystemWeb
{
    public class MikeModule : IHttpModule
    {
        private static MikeIds _mike;

        static MikeModule()
        {
            _mike = new MikeIds();
        }

        public static MikeIds Mike
        {
            get { return _mike; }
            set { _mike = value; }
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

            var mike = _mike;
            if (mike != null)
            {
                IRequestContext requestcontext = new SystemWebRequest(request);
                var report = mike.Analyze(requestcontext);
                if (report.ActionRequired)
                {
                    IPlatform platform = new SystemWebPlatform(context);
                    bool canContinue = await mike.TakeActionBasedOnReportAsync(report, platform);
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
