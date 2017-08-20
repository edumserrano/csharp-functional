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

namespace HttpResultMonad.HttpResultOnHttpClient.Tests
{
    public class HttpResultClientTests
    {
        [Fact]
        public void Contructor_null_HttpResponseMessage_throws_ArgumentNullException()
        {
            var exception = Should.Throw<ArgumentNullException>(() => new HttpResultClient(null));
            exception.Message.ShouldContain("httpClient");
        }

        [Fact]
        public void Dispose_can_be_called_multiple_times()
        {
            var httpResultClient = new HttpResultClient(new HttpClient());
            httpResultClient.Dispose();
            httpResultClient.Dispose();
            httpResultClient.Dispose();
        }

        [Fact]
        public async Task SendAsync_when_response_status_is_success()
        {
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://www.github.com")
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
            var testHandler = new TestHttpClientHandler(() => Task.FromResult(testHttpResponseMessage));
            var httpClient = new HttpClient(testHandler);

            var httpResultClient = new HttpResultClient(httpClient);
            var httpResult = await httpResultClient.SendAsync(testHttpRequestMessage);
            var httpClientState = httpResult.HttpState;


            httpClientState.Url.ShouldBe(testHttpRequestMessage.RequestUri);
            httpClientState.HttpMethod.ShouldBe(testHttpRequestMessage.Method.ToString());
            httpClientState.RequestContentLength.ShouldBe(testHttpRequestMessage.Content.Headers.ContentLength);
            httpClientState.RequestHeaders.HeadersEquals(testHttpRequestMessage.Headers.ToList()).ShouldBeTrue();
            
            httpClientState.HttpStatusCode.ShouldBe((int)testHttpResponseMessage.StatusCode);
            httpClientState.ResponseContentLength.ShouldBe(testHttpResponseMessage.Content.Headers.ContentLength);
            httpClientState.ResponseHeaders.HeadersEquals(testHttpResponseMessage.Headers.ToList()).ShouldBeTrue();
        }

        [Fact]
        public async Task SendAsync_when_response_status_is_false()
        {
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://www.github.com")
            {
                Content = new StringContent("request content", Encoding.UTF8, "application/text"),
                Headers = { { "header1", "value1" }, { "header2", new List<string> { "value2A", "value2B" } } }
            };
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                RequestMessage = testHttpRequestMessage,
                Content = new StringContent("response content", Encoding.UTF8, "application/text"),
                Headers = { { "header1", "value1" }, { "header2", new List<string> { "value2A", "value2B" } } }
            };
            var testHandler = new TestHttpClientHandler(() => Task.FromResult(testHttpResponseMessage));
            var httpClient = new HttpClient(testHandler);

            var httpResultClient = new HttpResultClient(httpClient);
            var httpResult = await httpResultClient.SendAsync(testHttpRequestMessage);
            var httpClientState = httpResult.HttpState;


            httpClientState.Url.ShouldBe(testHttpRequestMessage.RequestUri);
            httpClientState.HttpMethod.ShouldBe(testHttpRequestMessage.Method.ToString());
            httpClientState.RequestContentLength.ShouldBe(testHttpRequestMessage.Content.Headers.ContentLength);
            httpClientState.RequestHeaders.HeadersEquals(testHttpRequestMessage.Headers.ToList()).ShouldBeTrue();

            httpClientState.HttpStatusCode.ShouldBe((int)testHttpResponseMessage.StatusCode);
            httpClientState.ResponseContentLength.ShouldBe(testHttpResponseMessage.Content.Headers.ContentLength);
            httpClientState.ResponseHeaders.HeadersEquals(testHttpResponseMessage.Headers.ToList()).ShouldBeTrue();
        }
    }
}