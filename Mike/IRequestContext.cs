using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mike
{
    public interface IRequestContext
    {
        string Method { get; }
        string Path { get; }
        string QueryString { get; }
        string Scheme { get; }
        string RemoteIpAddress { get; }
        string LocalIpAddress { get; }
        IDictionary<string, string[]> Headers { get; }
    }
}
