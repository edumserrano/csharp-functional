using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HttpResultMonad.State;

namespace Tests.Shared
{
    internal class HttpStateTest : IEquatable<HttpStateTest>, IHttpState
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
            RequestBodyStr = requestBody.ReadAsString();
            _responseBody = responseBody;
            ResponseBodyStr = responseBody.ReadAsString();
        }

        public Uri Url { get; }

        public string HttpMethod { get; }

        public int HttpStatusCode { get; }

        public long? RequestContentLength { get; }

        public long? ResponseContentLength { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; }

        public string RequestBodyStr { get; }

        public string ResponseBodyStr { get; }

        public async Task<Stream> GetRequestBodyAsync()
        {
            var copyStream = new MemoryStream();
            await _requestBody.CopyToAsync(copyStream);
            _requestBody.Position = 0;
            copyStream.Position = 0;
            return copyStream;
        }

        public Task<Stream> GetResponseBodyAsync()
        {
            return Task.FromResult(_responseBody);
        }
        
        public bool Equals(HttpStateTest other)
        {
            return Equals(Url, other.Url)
                   && Equals(HttpMethod, other.HttpMethod)
                   && HttpStatusCode == other.HttpStatusCode
                   && HeadersEquals(RequestHeaders, other.RequestHeaders)
                   && HeadersEquals(ResponseHeaders, other.ResponseHeaders)
                   && string.Equals(RequestBodyStr, other.RequestBodyStr)
                   && string.Equals(ResponseBodyStr, other.ResponseBodyStr);
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
                hashCode = (hashCode * 397) ^ (_requestBody?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ResponseHeaders?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (_responseBody?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}