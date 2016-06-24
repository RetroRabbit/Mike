using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mike
{
    public enum IntrusionAction
    {
        ReturnChallenge,
        Throttle,
        ReturnServiceUnavailable,
        ReturnTooManyRequests,
        ReturnInternalServerError,
        None
    }
}
