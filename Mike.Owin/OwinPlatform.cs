using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Bouncer.Owin
{
    internal class OwinPlatform : IPlatform
    {
        private IDictionary<string, object> _environment;

        public OwinPlatform(IDictionary<string, object> environment)
        {
            _environment = environment;
        }

        public void RewriteRemoteIpAddress(string newAddress)
        {
            _environment["server.RemoteIpAddress"] = newAddress;

            object httpcontext = _environment.Get("System.Web.HttpContextBase");
            if (httpcontext != null)
            {
                SetNewRemoteIpAddressForHttpContext(newAddress, httpcontext);
            }
        }

        public Task RewriteResponseAsync(IResponse newResponse, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        private static void SetNewRemoteIpAddressForHttpContext(string newAddress, object context)
        {
            try
            {
                //TODO: this is slow
                if (context != null)
                {
                    var request = context.GetType().GetProperty("Request")?.GetValue(context);
                    if (request != null)
                    {
                        var serverVariables = request.GetType().GetProperty("ServerVariables")?.GetValue(request) as NameValueCollection;
                        if (serverVariables != null)
                        {
                            serverVariables["REMOTE_ADDR"] = newAddress;
                            serverVariables["REMOTE_HOST"] = newAddress;
                        }
                    }
                }
            }
            catch { }
        }
    }
}