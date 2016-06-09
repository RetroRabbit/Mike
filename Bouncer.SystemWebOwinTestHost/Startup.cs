using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Bouncer.SystemWebOwinTestHost.Startup))]

namespace Bouncer.SystemWebOwinTestHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseBouncer();
        }
    }
}
