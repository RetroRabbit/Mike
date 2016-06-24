using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Bouncer
{
    public interface IResponse
    {
        IEnumerable<KeyValuePair<string,string>> Headers { get; }
        int StatusCode { get; }
        string ReasonPhrase { get; }
        Stream ResponseBody { get; }
    }
}