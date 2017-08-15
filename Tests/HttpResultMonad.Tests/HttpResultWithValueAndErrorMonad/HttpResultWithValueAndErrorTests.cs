using System;
using System.Collections.Generic;
using HttpResultMonad.State;
using MaybeMonad;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueAndErrorMonad
{
    public class HttpResultWithValueAndErrorTests
    {
        [Fact]
        public void Creating_ok_HttpResultWithValueAndError_with_null_value_throws_exception()
        {
            var exception = Should.Throw<ArgumentNullException>(() => HttpResult.Ok<string, string>(null));
            exception.Message.ShouldStartWith(HttpResultMessages.SuccessResultMustHaveValue);
        }

        [Fact]
        public void Creating_fail_HttpResultWithValueAndError_with_null_error_throws_exception()
        {
            var exception = Should.Throw<ArgumentNullException>(() => HttpResult.Fail<string, string>(null));
            exception.Message.ShouldStartWith(HttpResultMessages.FailureResultMustHaveError);
        }

        [Fact]
        public void Ok_HttpResultWithValueAndError_IsSuccess_is_true()
        {
            var result = HttpResult.Ok<string, string>("abc");
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_HttpResultWithValueAndError_IsFailure_is_false()
        {
            var result = HttpResult.Ok<string, string>("abc");
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_HttpResultWithValueAndError_IsSuccess_is_false()
        {
            var result = HttpResult.Fail<string, string>("abc");
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_HttpResultWithValueAndError_IsFailure_equals_true()
        {
            var result = HttpResult.Fail<string, string>("abc");
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_ok_HttpResultWithValueAndError_returns_value()
        {
            var value = "abc";
            var result = HttpResult.Ok<string, string>(value);
            var isEqual = result.Value.Equals(value);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_fail_HttpResultWithValueAndError_throws_exception()
        {
            var result = HttpResult.Fail<string, string>("abc");
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Value;
            });
        }

        [Fact]
        public void Acessing_the_error_of_fail_HttpResultWithValueAndError_returns_error()
        {
            var error = "abc";
            var result = HttpResult.Fail<string, string>(error);
            var isEqual = result.Error.Equals(error);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_error_of_ok_HttpResultWithValueAndError_throws_exception()
        {
            var result = HttpResult.Ok<string, string>("abc");
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Error;
            });
        }

        [Fact]
        public void OK_HttpResult_withouth_passing_in_HttpState_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResult.Ok<string, string>("value");
            httpResult.HttpState.ShouldBe(Maybe<HttpState>.Nothing);
        }

        [Fact]
        public void Fail_HttpResult_withouth_passing_in_HttpState_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResult.Fail<string, string>("error");
            httpResult.HttpState.ShouldBe(Maybe<HttpState>.Nothing);
        }

        [Fact]
        public void From_if_predicate_is_true_returns_ok_HttpResultWithValueAndError_with_value()
        {
            var value = 1;
            var error = "error";
            var result = HttpResult.From(() => true, value, error);
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(value);
        }

        [Fact]
        public void From_if_predicate_is_false_returns_fail_HttpResultWithValueAndError_with_errir()
        {
            var value = 1;
            var error = "error";
            var result = HttpResult.From(() => false, value, error);
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(error);
        }

        [Fact]
        public void Combine_if_all_results_are_ok_returns_ok_result()
        {
            var resultsLists = new List<HttpResult<int, string>>
            {
                HttpResult.Ok<int,string>(1),
                HttpResult.Ok<int,string>(2),
                HttpResult.Ok<int,string>(3)
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Combine_returns_first_fail_HttpResultWithValueAndError_if_at_least_one_HttpResultWithValueAndError_is_a_fail()
        {
            var firstHttpState = Test.CreateHttpStateA();
            var secondHttpState = Test.CreateHttpStateB();

            var firstFailure = HttpResult.Fail<int, string>("error", firstHttpState);
            var resultsLists = new List<HttpResult<int, string>>
            {
                HttpResult.Ok<int,string>(1),
                firstFailure,
                HttpResult.Ok<int,string>(2),
                HttpResult.Fail<int,string>("secod error",secondHttpState)
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
            combinedResult.Error.ShouldBe(firstFailure.Error);
            combinedResult.HttpState.ShouldBe(firstFailure.HttpState);
        }
    }
}
