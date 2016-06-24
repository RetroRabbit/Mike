using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouncer
{
    internal static class HelperExtensions
    {
        public static string GetHeader(this IRequestContext request, string header)
        {
            string[] values = null;
            request.Headers?.TryGetValue(header, out values);
            return values?.FirstOrDefault();
        }
    }
}
