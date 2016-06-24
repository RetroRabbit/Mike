using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Mike.SystemWebOwinTestHost.Startup))]

namespace Mike.SystemWebOwinTestHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseBouncer();
        }
    }
}
