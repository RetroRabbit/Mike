using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mike
{
    public interface IResponse
    {
        IEnumerable<KeyValuePair<string,string>> Headers { get; }
        int StatusCode { get; }
        string ReasonPhrase { get; }
        Task WriteResponse(Stream writeTo, CancellationToken cancellationToken);
    }
}