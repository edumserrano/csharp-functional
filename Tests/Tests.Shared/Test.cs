using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using HttpResultMonad.State;

namespace Tests.Shared
{
    public static class Test
    {
        public static IHttpState CreateHttpStateA()
        {
            var requestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("content-type", new[] { "typeA", "typeB"})
            };

            var responseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("content-type", new[] {"typeA", "typeB"})
            };

            var state = new HttpStateTestBuilder()
                .WithHttpMethod(HttpMethod.Get)
                .WithUrl(new Uri("https://soundcloud.com"))
                .WithHttpStatusCode(HttpStatusCode.OK)
                .WithRequestBody("raw request body A")
                .WithResponseBody("raw response body A")
                .WithRequestHeaders(requestHeaders)
                .WithResponseHeaders(responseHeaders)
                .Build();
            
            return state;
        }

        public static IHttpState CreateHttpStateB()
        {
            return new HttpStateTestBuilder()
                .WithHttpMethod(HttpMethod.Delete)
                .WithUrl(new Uri("https://google.com"))
                .WithHttpStatusCode(HttpStatusCode.Forbidden)
                .WithRequestBody("raw request body B")
                .WithResponseBody("raw response body B")
                .Build();
        }
    }
}
