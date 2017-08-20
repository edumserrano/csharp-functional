using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpResultMonad.State;

namespace Tests.Shared
{
    public class HttpStateTest : IEquatable<HttpStateTest>, IHttpState
    {
        private readonly string _requestBodyStr;
        private readonly string _responseBodyStr;

        public HttpStateTest(
            Uri url, string httpMethod,
            int httpStatusCode,
            long? requestContentLength,
            long? responseContentLength,
            List<KeyValuePair<string, IEnumerable<string>>> requestHeaders,
            List<KeyValuePair<string, IEnumerable<string>>> responseHeaders,
            string requestBody,
            string responseBody)
        {
            Url = url;
            HttpMethod = httpMethod;
            HttpStatusCode = httpStatusCode;
            RequestContentLength = requestContentLength;
            ResponseContentLength = responseContentLength;
            RequestHeaders = requestHeaders;
            ResponseHeaders = responseHeaders;
            _requestBodyStr = requestBody;
            _responseBodyStr = responseBody;
        }

        public Uri Url { get; }

        public string HttpMethod { get; }

        public int HttpStatusCode { get; }

        public long? RequestContentLength { get; }

        public long? ResponseContentLength { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; }


        public Task<Stream> ReadRequestBodyAsStreamAsync()
        {
            return Task.FromResult(_requestBodyStr.ToStream());
        }

        public Task<Stream> ReadResponseBodyAsStreamAsync()
        {
            return Task.FromResult(_responseBodyStr.ToStream());
        }

        public Task<string> ReadRequestBodyAsStringAsync()
        {
            return Task.FromResult(_requestBodyStr);
        }

        public Task<string> ReadResponseBodyAsStringAsync()
        {
            return Task.FromResult(_responseBodyStr);
        }

        public Task<byte[]> ReadRequestBodyAsByteArrayAsync()
        {
            return Task.FromResult(Encoding.UTF8.GetBytes(_requestBodyStr));
        }

        public Task<byte[]> ReadResponseBodyAsByteArrayAsync()
        {
            return Task.FromResult(Encoding.UTF8.GetBytes(_responseBodyStr));
        }

        public bool Equals(HttpStateTest other)
        {
            return Equals(Url, other.Url)
                   && Equals(HttpMethod, other.HttpMethod)
                   && HttpStatusCode == other.HttpStatusCode
                   && HeadersEquals(RequestHeaders, other.RequestHeaders)
                   && HeadersEquals(ResponseHeaders, other.ResponseHeaders)
                   && string.Equals(_requestBodyStr, other._requestBodyStr)
                   && string.Equals(_responseBodyStr, other._responseBodyStr);
        }

        private bool HeadersEquals(
            List<KeyValuePair<string, IEnumerable<string>>> requestHeaders,
            List<KeyValuePair<string, IEnumerable<string>>> otherRequestHeaders)
        {
            if (requestHeaders.Count != otherRequestHeaders.Count)
            {
                return false;
            }

            foreach (var requestHeader in requestHeaders)
            {
                var keyMatched = false;

                var key = requestHeader.Key;
                var values = requestHeader.Value.ToList();

                foreach (var otherRequestHeader in otherRequestHeaders)
                {
                    var otherKey = otherRequestHeader.Key;
                    if (string.Equals(key, otherKey))
                    {
                        keyMatched = true;
                        var otherValues = otherRequestHeader.Value.ToList();
                        if (!values.SequenceEqual(otherValues))
                        {
                            return false;
                        }
                        break;
                    }
                }

                if (!keyMatched)
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            var otherHttpState = obj as HttpStateTest;
            return otherHttpState != null && Equals(otherHttpState);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (HttpMethod != null ? HttpMethod.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)HttpStatusCode;
                hashCode = (hashCode * 397) ^ (RequestHeaders?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (_requestBodyStr?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ResponseHeaders?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (_responseBodyStr?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public void Dispose() { }
    }
}