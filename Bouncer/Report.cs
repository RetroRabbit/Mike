using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouncer
{
    public class Report
    {
        public bool ActionRequired { get { return RemoteAddressRewriteAdvised || ChallengeAdvised || ThrottlingAdvised; } }
        public bool RemoteAddressRewriteAdvised { get; set; }
        public bool ChallengeAdvised { get; set; }
        public bool ThrottlingAdvised { get; set; }
        public string RealRemoteAddress { get; set; }
        public TimeSpan AnalysisDuration { get; set; }
    }
}
