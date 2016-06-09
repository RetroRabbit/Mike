using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bouncer.SystemWeb
{
    public class BouncerModule : IHttpModule
    {
        private static BouncerManager _bouncer;

        static BouncerModule()
        {
            _bouncer = new BouncerManager();
        }

        public static BouncerManager BouncerManager
        {
            get { return _bouncer; }
            set { _bouncer = value; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += Application_BeginRequest;
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            var bouncer = _bouncer;
            if(bouncer != null)
            {
                //TODO:stuff
            }
        }
    }
}
