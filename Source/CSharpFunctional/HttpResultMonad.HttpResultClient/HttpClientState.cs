using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HttpResultMonad.State;

namespace HttpResultMonad.HttpResultClient
{
    public struct HttpClientHttpState : IHttpState
    {
        private readonly HttpResponseMessage _response;
        private readonly HttpRequestMessage _request;

        public HttpClientHttpState(HttpResponseMessage response)
        {
            _response = response;
            _request = response.RequestMessage;
        }
        
        public Uri Url => _request.RequestUri;

        public string HttpMethod => _request.Method.ToString();

        public int HttpStatusCode => (int)_response.StatusCode;

        public long? RequestContentLength => _request.Content.Headers.ContentLength;
        
        public long? ResponseContentLength => _response.Content.Headers.ContentLength;

        public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders => _request.Headers.ToList();

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders => _response.Headers.ToList();

        public Task<Stream> GetRequestBodyAsync()
        {
            return _request.Content
                .ReadAsStreamAsync();
        }

        public Task<Stream> GetResponseBodyAsync()
        {
            return _response.Content
                .ReadAsStreamAsync();
        }

        public void Dispose()
        {
            _request.Dispose();
            _response?.Dispose();
        }
    }
}
