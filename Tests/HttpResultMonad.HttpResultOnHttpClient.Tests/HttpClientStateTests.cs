using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Tests.Shared;
using Xunit;
using HttpResultMonad.State;

namespace HttpResultMonad.HttpResultOnHttpClient.Tests
{
    public class HttpClientStateTests
    {
        [Fact]
        public void Contructor_null_HttpResponseMessage_throws_ArgumentNullException()
        {
            var exception = Should.Throw<ArgumentNullException>(() => new HttpClientState(null));
            exception.Message.ShouldContain("response");
        }

        [Fact]
        public void Contructor_data_is_populated_from_HttpResponseMessage()
        {
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "www.github.com")
            {
                Content = new StringContent("request content", Encoding.UTF8, "application/text"),
                Headers = { { "header1", "value1" }, { "header2", new List<string> { "value2A", "value2B" } } }
            };
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = testHttpRequestMessage,
                Content = new StringContent("response content", Encoding.UTF8, "application/text"),
                Headers = { { "header1", "value1" }, { "header2", new List<string> { "value2A", "value2B" } } }
            };

            var httpClientState = new HttpClientState(testHttpResponseMessage);

            httpClientState.Url.ShouldBe(testHttpRequestMessage.RequestUri);
            httpClientState.HttpMethod.ShouldBe(testHttpRequestMessage.Method.ToString());
            httpClientState.RequestContentLength.ShouldBe(testHttpRequestMessage.Content.Headers.ContentLength);
            httpClientState.RequestHeaders
                .EqualsHeaders(testHttpRequestMessage.Headers.Concat(testHttpRequestMessage.Content.Headers).ToList())
                .ShouldBeTrue();

            httpClientState.HttpStatusCode.ShouldBe((int)testHttpResponseMessage.StatusCode);
            httpClientState.ResponseContentLength.ShouldBe(testHttpResponseMessage.Content.Headers.ContentLength);
            httpClientState.ResponseHeaders
                .EqualsHeaders(testHttpResponseMessage.Headers.Concat(testHttpResponseMessage.Content.Headers).ToList())
                .ShouldBeTrue();
        }

        [Fact]
        public void Dispose_can_be_called_multiple_times()
        {
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "www.github.com")
            {
                Content = new StringContent("request content", Encoding.UTF8, "application/text"),
            };
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = testHttpRequestMessage,
                Content = new StringContent("response content", Encoding.UTF8, "application/text"),
            };

            var httpClientState = new HttpClientState(testHttpResponseMessage);
            httpClientState.Dispose();
            httpClientState.Dispose();
            httpClientState.Dispose();
        }

        [Fact]
        public async Task ReadRequestBodyAsStringAsync_returns_data_from_HttpResponseMessage()
        {
            var reqContent = "request content";
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "www.github.com")
            {
                Content = new StringContent(reqContent, Encoding.UTF8, "application/text"),
            };
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = testHttpRequestMessage
            };

            var httpClientState = new HttpClientState(testHttpResponseMessage);
            var stateReqBody = await httpClientState.ReadRequestBodyAsStringAsync();
            stateReqBody.ShouldBe(reqContent);
        }

        [Fact]
        public async Task ReadRequestBodyAsStreamAsync_returns_data_from_HttpResponseMessage()
        {
            var reqContent = "request content";
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "www.github.com")
            {
                Content = new StringContent(reqContent, Encoding.UTF8, "application/text"),
            };
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = testHttpRequestMessage
            };

            var httpClientState = new HttpClientState(testHttpResponseMessage);
            var stateReqBody = await httpClientState.ReadRequestBodyAsStreamAsync();
            stateReqBody
                .ReadAsString()
                .ShouldBe(reqContent);
        }

        [Fact]
        public async Task ReadRequestBodyAsByteArrayAsync_returns_data_from_HttpResponseMessage()
        {
            var reqContent = "request content";
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "www.github.com")
            {
                Content = new StringContent(reqContent, Encoding.UTF8, "application/text"),
            };
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = testHttpRequestMessage
            };

            var httpClientState = new HttpClientState(testHttpResponseMessage);
            var stateReqBody = await httpClientState.ReadRequestBodyAsByteArrayAsync();
            stateReqBody
                .ReadAsString()
                .ShouldBe(reqContent);
        }

        [Fact]
        public async Task ReadResponseBodyAsStringAsync_returns_data_from_HttpResponseMessage()
        {
            var respContent = "response content";
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "www.github.com");
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = testHttpRequestMessage,
                Content = new StringContent(respContent, Encoding.UTF8, "application/text"),
            };

            var httpClientState = new HttpClientState(testHttpResponseMessage);
            var stateRespBody = await httpClientState.ReadResponseBodyAsStringAsync();
            stateRespBody.ShouldBe(respContent);
        }

        [Fact]
        public async Task ReadResponseBodyAsStreamAsync_returns_data_from_HttpResponseMessage()
        {
            var respContent = "response content";
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "www.github.com");
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = testHttpRequestMessage,
                Content = new StringContent(respContent, Encoding.UTF8, "application/text"),
            };

            var httpClientState = new HttpClientState(testHttpResponseMessage);
            var stateRespBody = await httpClientState.ReadResponseBodyAsStreamAsync();
            stateRespBody
                .ReadAsString()
                .ShouldBe(respContent);
        }

        [Fact]
        public async Task ReadResponseBodyAsByteArrayAsync_returns_data_from_HttpResponseMessage()
        {
            var respContent = "response content";
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "www.github.com");
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = testHttpRequestMessage,
                Content = new StringContent(respContent, Encoding.UTF8, "application/text"),
            };

            var httpClientState = new HttpClientState(testHttpResponseMessage);
            var stateRespBody = await httpClientState.ReadResponseBodyAsByteArrayAsync();
            stateRespBody
                .ReadAsString()
                .ShouldBe(respContent);
        }


    }
}
