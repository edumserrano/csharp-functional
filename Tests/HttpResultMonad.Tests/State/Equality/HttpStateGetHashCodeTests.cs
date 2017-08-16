using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using HttpResultMonad.State;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.State.Equality
{
    [Trait("HttpResultMonads", "HttpState")]
    public class HttpStateGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_two_HttpState_is_equal_if_all_properties_are_equal()
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

            var httpState1 = new HttpState(builder);
            var httpState2 = new HttpState(builder);

            httpState1.GetHashCode().ShouldBe(httpState2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_HttpState_is_false_if_at_least_one_property_is_not_equal()
        {
            var url = new Uri("https://github.com");
            var url2 = new Uri("https://microsoft.com");
            var httpMethod = HttpMethod.Get;

            var builder = new HttpStateBuilder();
            builder = builder
                .WithUrl(url)
                .WithHttpMethod(httpMethod);
            var httpState1 = new HttpState(builder);

            var builder2 = new HttpStateBuilder();
            builder2 = builder2
                .WithUrl(url2)
                .WithHttpMethod(httpMethod);
            var httpState2 = new HttpState(builder2);

            httpState1.GetHashCode().ShouldNotBe(httpState2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_empty_HttpState_is_true()
        {
            var httpState1 = new HttpState();
            var httpState2 = new HttpState();

            httpState1.GetHashCode().ShouldBe(httpState2.GetHashCode());
        }
    }
}
