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
        public static IAppBuilder UseMike(this IAppBuilder app)
        {
            return app.UseMike(new MikeIds());
        }

        public static IAppBuilder UseMike(this IAppBuilder app, MikeConfiguration configuration)
        {
            return app.UseMike(new MikeIds(configuration));
        }

        public static IAppBuilder UseMike(this IAppBuilder app, MikeIds bouncer)
        {
            app.Use(typeof(MikeMiddleware), bouncer);

            return app;
        }
    }
}
