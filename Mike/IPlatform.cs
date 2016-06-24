using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mike
{
    public interface IPlatform
    {
        void RewriteRemoteIpAddress(string newIpAddress);
        Task RewriteResponseAsync(IResponse newResponse, CancellationToken cancellationToken);
    }
}
