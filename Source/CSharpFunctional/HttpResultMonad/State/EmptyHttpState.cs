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

        public Task<Stream> ReadRequestBodyAsStreamAsync()
        {
            return Task.FromResult(Stream.Null);
        }

        public Task<Stream> ReadResponseBodyAsStreamAsync()
        {
            return Task.FromResult(Stream.Null);
        }

        public Task<string> ReadRequestBodyAsStringAsync()
        {
            return Task.FromResult(string.Empty);
        }

        public Task<string> ReadResponseBodyAsStringAsync()
        {
            return Task.FromResult(string.Empty);
        }

        public Task<byte[]> ReadRequestBodyAsByteArrayAsync()
        {
            return Task.FromResult(new byte[0]);
        }

        public Task<byte[]> ReadResponseBodyAsByteArrayAsync()
        {
            return Task.FromResult(new byte[0]);
        }

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
            return 0;
        }

        public void Dispose() { }
    }
}
