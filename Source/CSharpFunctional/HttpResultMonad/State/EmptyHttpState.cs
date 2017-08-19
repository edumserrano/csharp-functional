using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HttpResultMonad.State
{
    /*
    * The purpose of this class is just for syntax usage
    * It allows the following syntax: HttpState.Empty to instanciate an empty http state
    */
    public static class HttpState
    {
        public static IHttpState Empty => new EmptyHttpHttpState();
    }

    public class EmptyHttpHttpState : IEquatable<EmptyHttpHttpState>, IHttpState
    {
        internal EmptyHttpHttpState() { }

        public Uri Url { get; }

        public string HttpMethod { get; }

        public int HttpStatusCode { get; }

        public long? RequestContentLength { get; }

        public long? ResponseContentLength { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; }

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; }

        public Task<Stream> GetRequestBodyAsync()
        {
            return Task.FromResult(Stream.Null);
        }

        public Task<Stream> GetResponseBodyAsync()
        {
            return Task.FromResult(Stream.Null);
        }

        public void Dispose() { }

        public bool Equals(EmptyHttpHttpState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.GetType() == other.GetType();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as EmptyHttpHttpState);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (HttpMethod != null ? HttpMethod.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ HttpStatusCode;
                hashCode = (hashCode * 397) ^ RequestContentLength.GetHashCode();
                hashCode = (hashCode * 397) ^ ResponseContentLength.GetHashCode();
                hashCode = (hashCode * 397) ^ (RequestHeaders != null ? RequestHeaders.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ResponseHeaders != null ? ResponseHeaders.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
