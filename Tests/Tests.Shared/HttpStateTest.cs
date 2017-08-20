using System;
using System.Collections.Generic;
using System.IO;
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
            RequestHeaders = requestHeaders ?? new List<KeyValuePair<string, IEnumerable<string>>>();
            ResponseHeaders = responseHeaders ?? new List<KeyValuePair<string, IEnumerable<string>>>();
            _requestBodyStr = requestBody ?? string.Empty;
            _responseBodyStr = responseBody ?? string.Empty;
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

        public bool Equals(IHttpState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var otherHttpStateTest = other as HttpStateTest;
            return Equals(otherHttpStateTest);
        }

        public bool Equals(HttpStateTest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Url, other.Url)
                   && Equals(HttpMethod, other.HttpMethod)
                   && HttpStatusCode == other.HttpStatusCode
                   && RequestHeaders.EqualsHeaders(other.RequestHeaders)
                   && ResponseHeaders.EqualsHeaders(other.ResponseHeaders)
                   && string.Equals(_requestBodyStr, other._requestBodyStr)
                   && string.Equals(_responseBodyStr, other._responseBodyStr);
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
                hashCode = (hashCode * 397) ^ RequestHeaders.GetHashCodeForHeaders();
                hashCode = (hashCode * 397) ^ _requestBodyStr.GetHashCode();
                hashCode = (hashCode * 397) ^ ResponseHeaders.GetHashCodeForHeaders();
                hashCode = (hashCode * 397) ^ _responseBodyStr.GetHashCode();
                return hashCode;
            }
        }

        public void Dispose() { }
    }
}