using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mike
{
    public class MikeConfiguration
    {
        /// <summary>
        /// When set to <c>true</c> the remote address will not
        /// be rewritten if behind a load balancer.
        /// </summary>
        public bool DontAllowRewriteOfRemoteIpAddress { get; set; }

        /// <summary>
        /// The number of calculated requests per second at which to trigger throttling.
        /// </summary>
        public double RequestRateLimit { get; set; } = 5;

        /// <summary>
        /// The window for calculating request limit. The total number of requests will be
        /// measured over the indicated timespan and divided by the number of seconds to
        /// get the request rate.
        /// </summary>
        public TimeSpan RequestRateCalculationWindow { get; set; } = TimeSpan.FromSeconds(15);

        public IntrusionAction ActionWhenRateLimitReached { get; set; }
        public IntrusionAction ActionWhenRateLimitReachedXhr { get; set; }
        public IntrusionAction ActionWhenIntrusionDetected { get; set; }
        public IntrusionAction ActionWhenIntrusionDetectedXhr { get; set; }
    }
}
