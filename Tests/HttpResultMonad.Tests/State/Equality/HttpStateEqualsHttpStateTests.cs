using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using HttpResultMonad.State;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.State.Equality
{
    public class HttpStateEqualsHttpStateTests
    {
        [Fact]
        public void Equals_between_HttpState_and_null_is_false()
        {
            var builder = new HttpStateBuilder();
            builder = builder
                .WithUrl(new Uri("https://github.com"));
            var httpState = new HttpState(builder);

            var isEqual = httpState.Equals(null);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_HttpState_and_an_object_of_type_different_from_HttpState_is_false()
        {
            var builder = new HttpStateBuilder();
            builder = builder
                .WithUrl(new Uri("https://github.com"));
            var httpState = new HttpState(builder);

            var isEqual = httpState.Equals("some other object");
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_HttpState_is_true_if_all_properties_are_equal_and_headers_are_empty()
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

            var isEqual = httpState1.Equals(httpState2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_HttpState_is_true_if_all_properties_are_equal_and_headers_are_equal()
        {
            var url = new Uri("https://github.com");
            var httpMethod = HttpMethod.Get;
            var httpStatusCode = HttpStatusCode.Accepted;
            var requestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Authorization", new List<string>{"Bearer token"}),
                new KeyValuePair<string, IEnumerable<string>>("Content-Type", new List<string>{"application/json"})
            };
            var rawRequestBody = "raw request body";
            var responseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Content-Lenght", new List<string>{"1234"})
            };
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

            var isEqual = httpState1.Equals(httpState2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_HttpState_is_false_if_at_least_one_property_is_not_equal()
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

            var isEqual = httpState1.Equals(httpState2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_HttpState_is_false_if_request_headers_count_do_not_match()
        {
            var requestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
            var requestHeaders2 = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Authorization", new List<string>{"Bearer token"})
            }; ;

            var builder = new HttpStateBuilder();
            builder = builder
                .WithRequestHeaders(requestHeaders);
            var httpState1 = new HttpState(builder);

            var builder2 = new HttpStateBuilder();
            builder2 = builder2
                .WithRequestHeaders(requestHeaders2);
            var httpState2 = new HttpState(builder2);

            var isEqual = httpState1.Equals(httpState2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_HttpState_is_false_if_request_headers_keys_do_not_match()
        {
            var requestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Content-Type", new List<string>{"application/json"})
            };
            var requestHeaders2 = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Authorization", new List<string>{"Bearer token"})
            };

            var builder = new HttpStateBuilder();
            builder = builder
                .WithRequestHeaders(requestHeaders);
            var httpState1 = new HttpState(builder);

            var builder2 = new HttpStateBuilder();
            builder2 = builder2
                .WithRequestHeaders(requestHeaders2);
            var httpState2 = new HttpState(builder2);

            var isEqual = httpState1.Equals(httpState2);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_two_HttpState_is_false_if_request_headers_values_do_not_match()
        {
            var requestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Content-Type", new List<string>{"application/json"})
            };
            var requestHeaders2 = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Content-Type", new List<string>{"application/text"})
            };

            var builder = new HttpStateBuilder();
            builder = builder
                .WithRequestHeaders(requestHeaders);
            var httpState1 = new HttpState(builder);

            var builder2 = new HttpStateBuilder();
            builder2 = builder2
                .WithRequestHeaders(requestHeaders2);
            var httpState2 = new HttpState(builder2);

            var isEqual = httpState1.Equals(httpState2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_HttpState_is_false_if_response_headers_count_do_not_match()
        {
            var responseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
            var responseHeaders2 = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Content-Lenght", new List<string>{"1234"})
            };

            var builder = new HttpStateBuilder();
            builder = builder
                .WithResponseHeaders(responseHeaders);
            var httpState1 = new HttpState(builder);

            var builder2 = new HttpStateBuilder();
            builder2 = builder2
                .WithResponseHeaders(responseHeaders2);
            var httpState2 = new HttpState(builder2);

            var isEqual = httpState1.Equals(httpState2);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_two_HttpState_is_false_if_response_headers_keys_do_not_match()
        {
            var responseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Content-Type", new List<string>{"application/json"})
            };
            var responseHeaders2 = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Authorization", new List<string>{"Bearer token"})
            };

            var builder = new HttpStateBuilder();
            builder = builder
                .WithResponseHeaders(responseHeaders);
            var httpState1 = new HttpState(builder);

            var builder2 = new HttpStateBuilder();
            builder2 = builder2
                .WithResponseHeaders(responseHeaders2);
            var httpState2 = new HttpState(builder2);

            var isEqual = httpState1.Equals(httpState2);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_two_HttpState_is_false_if_reponse_headers_values_do_not_match()
        {
            var responseHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Content-Type", new List<string>{"application/json"})
            };
            var responseHeaders2 = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>("Content-Type", new List<string>{"application/text"})
            };

            var builder = new HttpStateBuilder();
            builder = builder
                .WithResponseHeaders(responseHeaders);
            var httpState1 = new HttpState(builder);

            var builder2 = new HttpStateBuilder();
            builder2 = builder2
                .WithResponseHeaders(responseHeaders2);
            var httpState2 = new HttpState(builder2);

            var isEqual = httpState1.Equals(httpState2);
            isEqual.ShouldBeFalse();
        }
    }
}
