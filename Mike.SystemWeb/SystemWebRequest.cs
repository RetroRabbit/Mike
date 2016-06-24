using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace Mike.SystemWeb
{
    internal class SystemWebRequest : IRequestContext
    {
        private HttpRequest _request;

        public SystemWebRequest(HttpRequest request)
        {
            _request = request;
        }

        public IDictionary<string, string[]> Headers
        {
            get
            {
                return _request.Headers.Keys
                    .Cast<string>()
                    .ToDictionary(x => x, x => new string[] { _request.Headers[x].ToString() });
            }
        }

        public string LocalIpAddress { get { return null; } }
        public string Method { get { return _request.HttpMethod; } }
        public string Path { get { return _request.Path; } }
        public string QueryString { get { return _request.Url.Query; } }
        public string RemoteIpAddress { get { return _request.UserHostAddress; } }
        public string Scheme { get { return _request.Url.Scheme; } }
    }
}