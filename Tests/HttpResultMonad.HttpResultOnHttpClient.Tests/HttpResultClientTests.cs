using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
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
            var expectedRequestBody = "request content";
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://www.github.com")
            {
                Content = new StringContent(expectedRequestBody, Encoding.UTF8, "application/text"),
                Headers = { { "header1", "value1" }, { "header2", new List<string> { "value2A", "value2B" } } }
            };
            var expectedResponseBody = "response content";
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = testHttpRequestMessage,
                Content = new StringContent(expectedResponseBody, Encoding.UTF8, "application/text"),
                Headers = { { "header1", "value1" }, { "header2", new List<string> { "value2A", "value2B" } } }
            };
            var testHandler = new TestHttpClientHandler(() => Task.FromResult(testHttpResponseMessage));
            var httpClient = new HttpClient(testHandler);

            var httpResultClient = new HttpResultClient(httpClient);
            var httpResult = await httpResultClient.SendAsync(testHttpRequestMessage);

            httpResult.IsSuccess.ShouldBeTrue();
            var httpStateRequestBody = await httpResult.HttpState.ReadRequestBodyAsStringAsync();
            var httpStateResponseBody = await httpResult.HttpState.ReadResponseBodyAsStringAsync();
            httpStateRequestBody.ShouldBe(expectedRequestBody);
            httpStateResponseBody.ShouldBe(expectedResponseBody);
        }

        [Fact]
        public async Task SendAsync_when_response_status_is_false()
        {
            var expectedRequestBody = "request content";
            var testHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://www.github.com")
            {
                Content = new StringContent(expectedRequestBody, Encoding.UTF8, "application/text"),
                Headers = { { "header1", "value1" }, { "header2", new List<string> { "value2A", "value2B" } } }
            };
            var expectedResponseBody = "response content";
            var testHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                RequestMessage = testHttpRequestMessage,
                Content = new StringContent(expectedResponseBody, Encoding.UTF8, "application/text"),
                Headers = { { "header1", "value1" }, { "header2", new List<string> { "value2A", "value2B" } } }
            };
            var testHandler = new TestHttpClientHandler(() => Task.FromResult(testHttpResponseMessage));
            var httpClient = new HttpClient(testHandler);
            
            var httpResultClient = new HttpResultClient(httpClient);
            var httpResult = await httpResultClient.SendAsync(testHttpRequestMessage);

            httpResult.IsSuccess.ShouldBeFalse();
            var httpStateRequestBody = await httpResult.HttpState.ReadRequestBodyAsStringAsync();
            var httpStateResponseBody = await httpResult.HttpState.ReadResponseBodyAsStringAsync();
            httpStateRequestBody.ShouldBe(expectedRequestBody);
            httpStateResponseBody.ShouldBe(expectedResponseBody);
        }
    }
}