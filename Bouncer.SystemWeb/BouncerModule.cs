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
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += Application_BeginRequest;
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
        }
    }
}
