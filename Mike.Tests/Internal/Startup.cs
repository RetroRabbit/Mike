﻿using Microsoft.Owin;
using Owin;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Mike.Tests.Internal
{
    using AppFunc = Func<IOwinContext, Func<Task>, Task>;
    internal class Startup
    {
        public static List<AppFunc> AdditionalMiddleware { get; } = new List<AppFunc>();
        public static MikeIds Mike { get; set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.UseMike(Mike ?? new MikeIds());
            app.UseWelcomePage("/");
            app.Use(async (ctx, next) =>
            {
                if(ctx.Request.Path.Value == "/ipaddress")
                {
                    await ctx.Response.WriteAsync(ctx.Request.RemoteIpAddress);
                }
                else
                {
                    await next();
                }
            });
            AdditionalMiddleware.ForEach(mw => app.Use(mw));
        }
    }
}