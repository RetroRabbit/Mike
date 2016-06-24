using Mike;
using Mike.Owin;
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
            return app.UseBouncer(new MikeIds());
        }

        public static IAppBuilder UseBouncer(this IAppBuilder app, MikeConfiguration configuration)
        {
            return app.UseBouncer(new MikeIds(configuration));
        }

        public static IAppBuilder UseBouncer(this IAppBuilder app, MikeIds bouncer)
        {
            app.Use(typeof(MikeMiddleware), bouncer);

            return app;
        }
    }
}
