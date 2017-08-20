using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HttpResultMonad.State;

namespace HttpResultMonad.HttpResultOnHttpClient
{
    public class HttpClientState : IHttpState
    {
        private readonly HttpResponseMessage _response;
        private readonly HttpRequestMessage _request;

        public HttpClientState(HttpResponseMessage response)
        {
            _response = response ?? throw new ArgumentNullException(nameof(response));
            _request = response.RequestMessage;
        }

        public HttpClientState(HttpRequestMessage request, HttpResponseMessage response)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
            _response = response ?? throw new ArgumentNullException(nameof(response));
        }

        public Uri Url => _request.RequestUri;

        public string HttpMethod => _request.Method.ToString();

        public int HttpStatusCode => (int)_response.StatusCode;

        public long? RequestContentLength => _request.Content.Headers.ContentLength;

        public long? ResponseContentLength => _response.Content.Headers.ContentLength;

        public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders => _request.Headers.ToList();

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders => _response.Headers.ToList();

        public Task<Stream> ReadRequestBodyAsStreamAsync()
        {
            return _request.Content
                .ReadAsStreamAsync();
        }

        public Task<Stream> ReadResponseBodyAsStreamAsync()
        {
            return _response.Content
                .ReadAsStreamAsync();
        }

        public Task<string> ReadRequestBodyAsStringAsync()
        {
            return _request.Content
                .ReadAsStringAsync();
        }

        public Task<string> ReadResponseBodyAsStringAsync()
        {
            return _response.Content
                .ReadAsStringAsync();
        }

        public Task<byte[]> ReadRequestBodyAsByteArrayAsync()
        {
            return _request.Content
                .ReadAsByteArrayAsync();
        }

        public Task<byte[]> ReadResponseBodyAsByteArrayAsync()
        {
            return _response.Content
                .ReadAsByteArrayAsync();
        }

        public void Dispose()
        {
            _request.Dispose();
            _response.Dispose();
        }
    }
}
