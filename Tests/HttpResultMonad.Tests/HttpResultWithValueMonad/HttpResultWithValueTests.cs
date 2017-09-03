using System;
using System.Collections.Generic;
using HttpResultMonad.State;
using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueMonad
{
    [Trait("Monad", "HttpResultWithValue")]
    public class HttpResultWithValueTests
    {
        [Fact]
        public void Constructor_null_httpState_throws_ArgumentNullException()
        {
            var exception = Should.Throw<ArgumentNullException>(() => HttpResult.Ok<string, int>("some value", null));
            exception.Message.ShouldContain("httpState");
        }

        [Fact]
        public void Dispose_can_be_called_multiple_times()
        {
            var httpResult = HttpResult.Ok<string, int>("some value");
            httpResult.Dispose();
            httpResult.Dispose();
            httpResult.Dispose();
        }

        [Fact]
        public void Creating_ok_HtttpResultWithValue_with_null_value_throws_exception()
        {
            var exception = Should.Throw<ArgumentNullException>(() => HttpResult.Ok<string>(null));
            exception.Message.ShouldStartWith(HttpResultMessages.SuccessResultMustHaveValue);
        }

        [Fact]
        public void Ok_result_IsSuccess_is_true()
        {
            var result = HttpResult.Ok("abc");
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_result_IsFailure_is_false()
        {
            var result = HttpResult.Ok("abc");
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsSuccess_is_false()
        {
            var result = HttpResult.Fail<string>();
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsFailure_equals_true()
        {
            var result = HttpResult.Fail<string>();
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_ok_result_returns_value()
        {
            var value = "abc";
            var result = HttpResult.Ok(value);
            var isEqual = result.Value.Equals(value);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_fail_result_throws_exception()
        {
            var result = HttpResult.Fail<string>();
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Value;
            });
        }

        [Fact]
        public void OK_HttpResult_withouth_passing_in_http_state_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResult.Ok("value");
            httpResult.HttpState.ShouldBe(HttpState.Empty);
        }

        [Fact]
        public void Fail_HttpResult_withouth_passing_in_http_state_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResult.Fail<string>();
            httpResult.HttpState.ShouldBe(HttpState.Empty);
        }

        [Fact]
        public void Combine_if_all_results_are_ok_returns_ok_result()
        {
            var resultsLists = new List<HttpResult<string>>
            {
                HttpResult.Ok("value"),
                HttpResult.Ok("value"),
                HttpResult.Ok("value")
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Combine_returns_first_fail_result_if_at_least_one_result_is_a_fail()
        {
            var httpState1 = Test.CreateHttpStateA();
            var httpState2 = Test.CreateHttpStateB();

            var firstFailure = HttpResult.Fail<string>(httpState1);
            var resultsLists = new List<HttpResult<string>>
            {
                HttpResult.Ok("value"),
                firstFailure,
                HttpResult.Ok("value"),
                HttpResult.Fail<string>(httpState2)
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
            combinedResult.HttpState.ShouldBe(firstFailure.HttpState);
        }
    }
}
