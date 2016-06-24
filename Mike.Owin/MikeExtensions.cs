using Bouncer;
using Bouncer.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
    public static class MikeExtensions
    {
        public static IAppBuilder UseBouncer(this IAppBuilder app)
        {
            return app.UseBouncer(new Mike());
        }

        public static IAppBuilder UseBouncer(this IAppBuilder app, MikeConfiguration configuration)
        {
            return app.UseBouncer(new Mike(configuration));
        }

        public static IAppBuilder UseBouncer(this IAppBuilder app, Mike bouncer)
        {
            app.Use(typeof(MikeMiddleware), bouncer);

            return app;
        }
    }
}
