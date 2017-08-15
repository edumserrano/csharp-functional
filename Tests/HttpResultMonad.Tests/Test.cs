using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using HttpResultMonad.State;
using MaybeMonad;

namespace HttpResultMonad.Tests
{
    internal static class Test
    {
        public static Maybe<HttpState> CreateHttpStateA()
        {
            var requestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("content-type", new[] { "typeA", "typeB"})
            };

            var responseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("content-type", new[] {"typeA", "typeB"})
            };

            return new HttpStateBuilder()
                .WithHttpMethod(HttpMethod.Get)
                .WithUrl(new Uri("https://soundcloud.com"))
                .WithHttpStatusCode(HttpStatusCode.OK)
                .WithRequestRawBody("raw request body A")
                .WithResponseRawBody("raw response body A")
                .WithRequestHeaders(requestHeaders)
                .WithResponseHeaders(responseHeaders)
                .Build();
        }

        public static Maybe<HttpState> CreateHttpStateB()
        {
            return new HttpStateBuilder()
                .WithHttpMethod(HttpMethod.Delete)
                .WithUrl(new Uri("https://google.com"))
                .WithHttpStatusCode(HttpStatusCode.Forbidden)
                .WithRequestRawBody("raw request body B")
                .WithResponseRawBody("raw response body B")
                .Build();
        }
    }
}
