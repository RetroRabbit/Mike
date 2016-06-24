using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mike.Owin
{
    public class OwinRequestContext : IRequestContext
    {
        private IDictionary<string, object> _environment;

        public IDictionary<string, string[]> Headers { get { return _environment.Get<IDictionary<string, string[]>>("owin.RequestHeaders"); } }
        public string Method { get { return _environment.Get<string>("owin.RequestMethod"); } }
        public string Path { get { return GetPath(); } }
        public string QueryString { get { return _environment.Get<string>("owin.RequestQueryString"); } }
        public string Scheme { get { return _environment.Get<string>("owin.RequestScheme"); } }
        public string RemoteIpAddress { get { return _environment.Get<string>("server.RemoteIpAddress"); } }
        public string LocalIpAddress { get { return _environment.Get<string>("server.LocalIpAddress"); } }

        public OwinRequestContext(IDictionary<string, object> environment)
        {
            _environment = environment;
        }

        private string GetPath()
        {
            var path = $"{_environment.Get<string>("owin.RequestPathBase")}{_environment.Get<string>("owin.RequestPath")}";

            var qs = _environment.Get<string>("owin.RequestQueryString");
            if (!string.IsNullOrEmpty(qs)) path += "?" + qs;

            return path;
        }

        private string GetUrl()
        {
            var headers = _environment.Get<IDictionary<string, string[]>>("owin.RequestHeaders");

            return $"{_environment.Get<string>("owin.RequestScheme")}://{headers?["Host"]?.FirstOrDefault() ?? "<unknown>"}{GetPath()}";
        }
    }
}
