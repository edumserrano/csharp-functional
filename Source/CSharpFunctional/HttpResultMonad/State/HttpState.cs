using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
namespace HttpResultMonad.State
{
    public struct HttpState : IEquatable<HttpState>
    {
        internal HttpState(HttpStateBuilder builder)
        {
            Url = builder.Url;
            HttpMethod = builder.HttpMethod;
            HttpStatusCode = builder.HttpStatusCode;
            RequestHeaders = builder.RequestHeaders;
            RequestRawBody = builder.RequestRawBody;
            ResponseHeaders = builder.ResponseHeaders;
            ResponseRawBody = builder.ResponseRawBody;
        }


        public Uri Url { get; }

        public HttpMethod HttpMethod { get; }

        public HttpStatusCode HttpStatusCode { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; }

        public string RequestRawBody { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; }

        public string ResponseRawBody { get; }

        public bool Equals(HttpState other)
        {
            return Equals(Url, other.Url)
                && Equals(HttpMethod, other.HttpMethod)
                && HttpStatusCode == other.HttpStatusCode
                && HeadersEquals(RequestHeaders, other.RequestHeaders)
                && string.Equals(RequestRawBody, other.RequestRawBody)
                && HeadersEquals(ResponseHeaders, other.ResponseHeaders)
                && string.Equals(ResponseRawBody, other.ResponseRawBody);
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
            return obj is HttpState && Equals((HttpState)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (HttpMethod != null ? HttpMethod.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)HttpStatusCode;
                hashCode = (hashCode * 397) ^ (RequestHeaders?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (RequestRawBody?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ResponseHeaders?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ResponseRawBody?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
