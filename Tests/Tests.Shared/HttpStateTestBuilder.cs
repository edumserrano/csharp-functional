using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Tests.Shared
{
    public class HttpStateTestBuilder
    {
        public HttpStateTestBuilder()
        {
            RequestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
            ResponseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
        }

        public Uri Url { get; private set; }

        public HttpMethod HttpMethod { get; private set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; private set; }

        public string RequestBody { get; private set; }

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; private set; }

        public string ResponseBody { get; private set; }

        public HttpStateTestBuilder WithUrl(Uri url)
        {
            Url = url;
            return this;
        }

        public HttpStateTestBuilder WithHttpMethod(HttpMethod httpMethod)
        {
            HttpMethod = httpMethod;
            return this;
        }

        public HttpStateTestBuilder WithHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
            return this;
        }

        public HttpStateTestBuilder WithRequestHeaders(List<KeyValuePair<string, IEnumerable<string>>> requestHeaders)
        {
            RequestHeaders = requestHeaders;
            return this;
        }

        public HttpStateTestBuilder WithResponseHeaders(List<KeyValuePair<string, IEnumerable<string>>> responseHeaders)
        {
            ResponseHeaders = responseHeaders;
            return this;
        }

        public HttpStateTestBuilder WithRequestBody(string requestRawBody)
        {
            RequestBody = requestRawBody;
            return this;
        }

        public HttpStateTestBuilder WithResponseBody(string responseRawBody)
        {
            ResponseBody = responseRawBody;
            return this;
        }

        public HttpStateTest Build()
        {
            return new HttpStateTest(
                url: Url,
                httpMethod: HttpMethod.ToString(),
                httpStatusCode: (int)HttpStatusCode,
                requestContentLength: RequestBody.ToStream().Length,
                responseContentLength: ResponseBody.ToStream().Length,
                requestHeaders: RequestHeaders,
                responseHeaders: ResponseHeaders,
                requestBody: RequestBody,
                responseBody: ResponseBody);
        }
    }
}
