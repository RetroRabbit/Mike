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
            return request.Headers?[header]?.FirstOrDefault();
        }
    }
}
