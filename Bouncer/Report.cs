using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouncer
{
    public class Report
    {
        public BouncerConfiguration Configuration { get; set; }
        public bool RemoteAddressRewriteAdvised { get; set; }
        public bool IntrusionDetected { get; set; }
        public double RequestRate { get; set; }
        public string RealRemoteAddress { get; set; }
        public TimeSpan AnalysisDuration { get; set; }
        public bool IsXhr { get; set; }

        public bool RateLimitReached { get { return Configuration?.RequestRateLimit > 0 && RequestRate > Configuration?.RequestRateLimit; } }
        public bool ActionRequired { get { return RemoteAddressRewriteAdvised || IntrusionDetected || RateLimitReached; } }
    }
}
