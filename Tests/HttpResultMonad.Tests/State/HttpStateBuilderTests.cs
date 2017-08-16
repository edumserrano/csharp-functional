using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HttpResultMonad.State;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.State
{
    [Trait("HttpResultMonads", "HttpState")]
    public class HttpStateBuilderTests
    {
        [Fact]
        public void Build_creates_HttpState_with_correct_values()
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
            var httpState = builder.Build();

            httpState.Url.ShouldBe(url);
            httpState.HttpMethod.ShouldBe(httpMethod);
            httpState.HttpStatusCode.ShouldBe(httpStatusCode);
            httpState.RequestHeaders.ShouldBe(requestHeaders);
            httpState.RequestRawBody.ShouldBe(rawRequestBody);
            httpState.ResponseRawBody.ShouldBe(rawResponseBody);
        }

        [Fact]
        public async Task Build_from_HttpResponse_creates_HttpState_with_correct_values()
        {
            var url = new Uri("https://github.com");
            var httpMethod = HttpMethod.Get;
            var httpStatusCode = HttpStatusCode.Accepted;
            var requestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Authorization", new List<string>{"Bearer token"})
            };
            var rawRequestBody = "raw request body";
            var responseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Content-Lenght", new List<string>{"1234"})
            };
            var rawResponseBody = "raw response body";

            var requestMessage = new HttpRequestMessage
            {
                RequestUri = url,
                Method = httpMethod,
                Content = new StringContent(rawRequestBody)
            };
            requestHeaders.ForEach(pair => requestMessage.Headers.Add(pair.Key, pair.Value));

            var httpResponse = new HttpResponseMessage
            {
                RequestMessage = requestMessage,
                Content = new StringContent(rawResponseBody),
                StatusCode = httpStatusCode
            };
            responseHeaders.ForEach(pair => httpResponse.Headers.Add(pair.Key, pair.Value));


            var httpState = await HttpStateBuilder.BuildAsync(httpResponse);

            httpState.Url.ShouldBe(url);
            httpState.HttpMethod.ShouldBe(httpMethod);
            httpState.HttpStatusCode.ShouldBe(httpStatusCode);
            httpState.RequestRawBody.ShouldBe(rawRequestBody);
            httpState.ResponseRawBody.ShouldBe(rawResponseBody);
            httpState.RequestHeaders[0].Key.ShouldBe(requestHeaders[0].Key);
            httpState.RequestHeaders[0].Value.ToList().ShouldBe(requestHeaders[0].Value.ToList());
            httpState.ResponseHeaders[0].Key.ShouldBe(responseHeaders[0].Key);
            httpState.ResponseHeaders[0].Value.ToList().ShouldBe(responseHeaders[0].Value.ToList());
        }

        [Fact]
        public async Task Build_from_HttpResponse_creates_HttpState_with_correct_values_when_request_and_response_bodies_are_null()
        {
            var url = new Uri("https://github.com");
            var httpMethod = HttpMethod.Get;
            var httpStatusCode = HttpStatusCode.Accepted;

            var requestMessage = new HttpRequestMessage
            {
                RequestUri = url,
                Method = httpMethod
            };

            var httpResponse = new HttpResponseMessage
            {
                RequestMessage = requestMessage,
                StatusCode = httpStatusCode
            };


            var httpState = await HttpStateBuilder.BuildAsync(httpResponse);

            httpState.Url.ShouldBe(url);
            httpState.HttpMethod.ShouldBe(httpMethod);
            httpState.HttpStatusCode.ShouldBe(httpStatusCode);
            httpState.RequestRawBody.ShouldBeEmpty();
            httpState.ResponseRawBody.ShouldBeEmpty();
        }
    }
}
