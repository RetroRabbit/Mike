using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Linq;

namespace Mike.SystemWeb
{
    internal class SystemWebPlatform : IPlatform
    {
        private HttpContext _context;

        public SystemWebPlatform(HttpContext context)
        {
            _context = context;
        }

        public void RewriteRemoteIpAddress(string newAddress)
        {
            _context.Request.ServerVariables["REMOTE_ADDR"] = newAddress;
            _context.Request.ServerVariables["REMOTE_HOST"] = newAddress;
        }

        public async Task RewriteResponseAsync(IResponse newResponse, CancellationToken cancellationToken)
        {
            var response = _context.Response;

            response.ClearHeaders();
            foreach (var header in newResponse.Headers) response.Headers.Add(header.Key, header.Value);
            response.StatusCode = newResponse.StatusCode;
            response.Status = newResponse.ReasonPhrase;
            await newResponse.WriteResponse(response.OutputStream, cancellationToken);
        }
    }
}