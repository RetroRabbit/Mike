using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bouncer
{
    public class Mike
    {
        public MikeConfiguration Configuration { get; set; }

        public Mike()
            : this(new MikeConfiguration())
        {
        }

        public Mike(MikeConfiguration configuration)
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

            string ipAddress = request.RemoteIpAddress;
            string forwardedFor = request.GetHeader("X-Forwarded-For");
            
            if(!string.IsNullOrEmpty(forwardedFor))
            {
                bool recognizedProxyAddress = IsRecognizedReverseProxyAddress(ipAddress);
                forwardedFor = RewriteForwardedAddress(forwardedFor, ipAddress, recognizedProxyAddress);
                if(!string.IsNullOrEmpty(forwardedFor) && ipAddress != forwardedFor)
                {
                    ipAddress = forwardedFor;
                    report.RemoteAddressRewriteAdvised = true;
                }

                report.ForwardedIpAddressWasRecognizedProxy = recognizedProxyAddress;
            }

            report.OriginalRemoteAddress = request.RemoteIpAddress;
            report.RemoteAddress = ipAddress;
            return report;
        }

        /// <summary>
        /// Rewrites the request IP address if a forwarded address is provided. By default this
        /// will return the forwarded IP address the original address is private or
        /// <paramref name="isRecognizedReverseProxyAddress"/> is true, and the forwarded address
        /// is not a private IP address. The address returned here will be set on
        /// <see cref="Report.RemoteAddress"/> for consideration to rewrite by
        /// <see cref="TakeActionBasedOnReportAsync"/>.
        /// </summary>
        /// <param name="forwardedAddress">The value of the forwarded address. Typically the value of the X-Forwarded-For header.</param>
        /// <param name="originalAddress">The original remote IP address of the connecting client.</param>
        /// <param name="isRecognizedReverseProxyAddress"><c>true</c> if <paramref name="originalAddress"/> was detected to be a recognized reverse proxy server IP address.</param>
        /// <returns>The Ip Address to use for the request.</returns>
        protected virtual string RewriteForwardedAddress(string forwardedAddress, string originalAddress, bool isRecognizedReverseProxyAddress)
        {
            bool originalAcceptable = IsLocalAddress(originalAddress) || isRecognizedReverseProxyAddress;
            bool forwardedAcceptable = !IsLocalAddress(forwardedAddress);

            if (originalAcceptable && forwardedAcceptable) return forwardedAddress;
            else return originalAddress;
        }

        /// <summary>
        /// When overriden in a derived class should return <c>true</c> if
        /// <paramref name="originalAddress"/> is a recognized reverse proxy server.
        /// </summary>
        /// <param name="originalAddress">The address of the connecting client.</param>
        /// <returns><c>true</c> if <paramref name="originalAddress"/> is a recognized reverse proxy server IP address.</returns>
        protected virtual bool IsRecognizedReverseProxyAddress(string originalAddress)
        {
            //TODO: find a way for this to work nicely
            return false;
        }

        private bool IsLocalAddress(string originalAddress)
        {
            //from http://stackoverflow.com/a/11327345
            return Regex.IsMatch(originalAddress, @"/(^127\.)|(^192\.168\.)|(^10\.)|(^172\.1[6-9]\.)|(^172\.2[0-9]\.)|(^172\.3[0-1]\.)|(^::1$)|(^[fF][cCdD])/");
        }

        /// <summary>
        /// Takes any actions recommended by a report. Returns a <see cref="bool"/> indicating
        /// whether or not the request may still be handled by downstream middleware. When <see cref="TakeActionBasedOnReportAsync(Report, Bouncer.IPlatform)"/>
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
        public async Task<bool> TakeActionBasedOnReportAsync(Report report, IPlatform platform)
        {
            if (report.RemoteAddressRewriteAdvised && !Configuration.DontAllowRewriteOfRemoteIpAddress)
            {
                platform.RewriteRemoteIpAddress(report.RemoteAddress);
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
