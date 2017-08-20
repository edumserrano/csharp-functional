using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HttpResultMonad.State
{
    public interface IHttpState
    {
        Uri Url { get; }

        string HttpMethod { get; }

        int HttpStatusCode { get; }

        long? RequestContentLength { get; }

        long? ResponseContentLength { get; }

        List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; }

        List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; }

        Task<Stream> GetRequestBodyAsync();

        Task<Stream> GetResponseBodyAsync();
    }
}