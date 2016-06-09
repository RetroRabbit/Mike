using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouncer
{
    public class BouncerManager
    {
        public BouncerConfiguration Configuration { get; set; }

        public BouncerManager()
            : this(new BouncerConfiguration())
        {
        }

        public BouncerManager(BouncerConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Analyze a request for potential intrusion or other problems.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Report Analyze(IRequestContext request)
        {
            var report = new Report();

            string ipAddress;
            string forwardedFor = request.GetHeader("X-Forwarded-For");
            if (!string.IsNullOrEmpty(forwardedFor) && forwardedFor != request.RemoteIpAddress)
            {
                ipAddress = forwardedFor;
                report.RemoteAddressRewriteAdvised = true;
            }
            else
            {
                ipAddress = request.RemoteIpAddress;
            }

            report.RealRemoteAddress = ipAddress;
            return report;
        }

        /// <summary>
        /// Takes any actions recommended by a report. Returns a <see cref="bool"/> indicating
        /// whether or not the request may still be handled by downstream middleware. When <see cref="ActionOnReportAsync(Report, Bouncer.IPlatform)"/>
        /// returns <c>false</c>, the request must end after the call. If it returns <c>true</c>
        /// the request must be handed to downstream middleware.
        /// </summary>
        /// <param name="report">The report to action on.</param>
        /// <param name="platform">A platform providing Bouncer a way to interact with the hosting environment.</param>
        /// <returns>
        /// <c>true</c> if downstream middleware must still handle the request, or <c>false</c>
        /// if the response has already been sent to the client and downstream middleware cannot
        /// handle the request anymore.
        /// </returns>
        public async Task<bool> ActionOnReportAsync(Report report, IPlatform platform)
        {
            if (report.RemoteAddressRewriteAdvised && !Configuration.DontAllowRewriteOfRemoteIpAddress)
            {
                platform.RewriteRemoteIpAddress(report.RealRemoteAddress);
            }

            IntrusionAction actionToTake = IntrusionAction.None;
            if (report.IntrusionDetected)
            {
                if(report.IsXhr)
                {
                    actionToTake = Configuration.ActionWhenIntrusionDetectedXhr;
                }
                else
                {
                    actionToTake = Configuration.ActionWhenIntrusionDetected;
                }
            }

            if (actionToTake == IntrusionAction.None && report.RateLimitReached)
            {
                if (report.IsXhr)
                {
                    actionToTake = Configuration.ActionWhenRateLimitReachedXhr;
                }
                else
                {
                    actionToTake = Configuration.ActionWhenRateLimitReached;
                }
            }

            if (actionToTake == IntrusionAction.None)
            {
                return true;
            }
            else
            {
                return await TakeActionAsync(report, actionToTake, platform);
            }
        }

        private Task<bool> TakeActionAsync(Report report, IntrusionAction actionToTake, IPlatform platform)
        {
            throw new NotImplementedException();
        }
    }
}
