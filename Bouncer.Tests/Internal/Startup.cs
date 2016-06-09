using Microsoft.Owin;
using Owin;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bouncer.Tests.Internal
{
    using AppFunc = Func<IOwinContext, Func<Task>, Task>;
    internal class Startup
    {
        public static List<AppFunc> AdditionalMiddleware { get; } = new List<AppFunc>();
        public static BouncerManager BouncerManager { get; set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.UseBouncer(BouncerManager ?? new BouncerManager());
            app.UseWelcomePage("/");
            AdditionalMiddleware.ForEach(mw => app.Use(mw));
        }
    }
}