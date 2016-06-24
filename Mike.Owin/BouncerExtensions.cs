using Bouncer;
using Bouncer.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
    public static class BouncerExtensions
    {
        public static IAppBuilder UseBouncer(this IAppBuilder app)
        {
            return app.UseBouncer(new BouncerManager());
        }

        public static IAppBuilder UseBouncer(this IAppBuilder app, BouncerConfiguration configuration)
        {
            return app.UseBouncer(new BouncerManager(configuration));
        }

        public static IAppBuilder UseBouncer(this IAppBuilder app, BouncerManager bouncer)
        {
            app.Use(typeof(BouncerMiddleware), bouncer);

            return app;
        }
    }
}
