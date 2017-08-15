using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using HttpResultMonad.State;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.State
{
    public class HttpStateTests
    {
        [Fact]
        public void Contructor_creates_HttpState_with_correct_values()
        {
            var url = new Uri("https://github.com");
            var httpMethod = HttpMethod.Get;
            var httpStatusCode = HttpStatusCode.Accepted;
            var requestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
            var rawRequestBody = "raw request body";
            var responseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
            var rawResponseBody = "raw response body";

            var builder = new HttpStateBuilder();
            builder = builder
                .WithUrl(url)
                .WithHttpMethod(httpMethod)
                .WithHttpStatusCode(httpStatusCode)
                .WithRequestHeaders(requestHeaders)
                .WithRequestRawBody(rawRequestBody)
                .WithResponseHeaders(responseHeaders)
                .WithResponseRawBody(rawResponseBody);
            var httpState = new HttpState(builder);

            httpState.Url.ShouldBe(url);
            httpState.HttpMethod.ShouldBe(httpMethod);
            httpState.HttpStatusCode.ShouldBe(httpStatusCode);
            httpState.RequestHeaders.ShouldBe(requestHeaders);
            httpState.RequestRawBody.ShouldBe(rawRequestBody);
            httpState.ResponseRawBody.ShouldBe(rawResponseBody);
            httpState.ResponseHeaders.ShouldBe(responseHeaders);
        }
    }
}
