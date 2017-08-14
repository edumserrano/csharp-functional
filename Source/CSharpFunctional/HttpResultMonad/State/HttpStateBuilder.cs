using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpResultMonad.State
{
    public class HttpStateBuilder
    {
        public HttpStateBuilder()
        {
            RequestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
            ResponseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
        }

        public Uri Url { get; private set; }

        public HttpMethod HttpMethod { get; private set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; private set; }

        public string RequestRawBody { get; private set; }

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; private set; }

        public string ResponseRawBody { get; private set; }

        public HttpStateBuilder WithUrl(Uri url)
        {
            Url = url;
            return this;
        }

        public HttpStateBuilder WithHttpMethod(HttpMethod httpMethod)
        {
            HttpMethod = httpMethod;
            return this;
        }

        public HttpStateBuilder WithHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
            return this;
        }

        public HttpStateBuilder WithRequestHeaders(List<KeyValuePair<string, IEnumerable<string>>> requestHeaders)
        {
            RequestHeaders = requestHeaders;
            return this;
        }

        public HttpStateBuilder WithResponseHeaders(List<KeyValuePair<string, IEnumerable<string>>> responseHeaders)
        {
            ResponseHeaders = responseHeaders;
            return this;
        }

        public HttpStateBuilder WithRequestRawBody(string requestRawBody)
        {
            RequestRawBody = requestRawBody;
            return this;
        }

        public HttpStateBuilder WithResponseRawBody(string responseRawBody)
        {
            ResponseRawBody = responseRawBody;
            return this;
        }

        public static async Task<HttpState> BuildAsync(HttpResponseMessage response)
        {
            var url = response.RequestMessage.RequestUri;
            var httpMethod = response.RequestMessage.Method;
            var httpStatusCode = response.StatusCode;
            var requestHeaders = response.RequestMessage.Headers.ToList();
            var requestRawBody = response.RequestMessage.Content == null
                ? string.Empty
                : await response.RequestMessage.Content.ReadAsStringAsync();
            var responseHeaders = response.Headers.ToList();
            var responseRawBody = response.Content == null
                ? string.Empty
                : await response.Content.ReadAsStringAsync();

            return new HttpStateBuilder()
                .WithUrl(url)
                .WithHttpMethod(httpMethod)
                .WithHttpStatusCode(httpStatusCode)
                .WithRequestHeaders(requestHeaders)
                .WithRequestRawBody(requestRawBody)
                .WithResponseHeaders(responseHeaders)
                .WithResponseRawBody(responseRawBody)
                .Build();
        }

        public HttpState Build()
        {
            return new HttpState(this);
        }
    }
}
