using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HttpResultMonad.State;

namespace Tests.Shared
{
    internal class HttpStateTest : IHttpState
    {
        private readonly Stream _requestBody;
        private readonly Stream _responseBody;

        public HttpStateTest(
            Uri url, string httpMethod,
            int httpStatusCode,
            long? requestContentLength,
            long? responseContentLength,
            List<KeyValuePair<string, IEnumerable<string>>> requestHeaders,
            List<KeyValuePair<string, IEnumerable<string>>> responseHeaders,
            Stream requestBody,
            Stream responseBody)
        {
            Url = url;
            HttpMethod = httpMethod;
            HttpStatusCode = httpStatusCode;
            RequestContentLength = requestContentLength;
            ResponseContentLength = responseContentLength;
            RequestHeaders = requestHeaders;
            ResponseHeaders = responseHeaders;
            _requestBody = requestBody;
            _responseBody = responseBody;
        }

        public Uri Url { get; }

        public string HttpMethod { get; }

        public int HttpStatusCode { get; }

        public long? RequestContentLength { get; }

        public long? ResponseContentLength { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; }

        public Task<Stream> GetRequestBodyAsync()
        {
            return Task.FromResult(_requestBody);
        }

        public Task<Stream> GetResponseBodyAsync()
        {
            return Task.FromResult(_responseBody);
        }

        public void Dispose() { }
    }
}