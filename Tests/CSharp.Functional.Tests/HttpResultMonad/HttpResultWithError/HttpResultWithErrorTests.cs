using System;
using System.Collections.Generic;
using HttpResultMonad;
using HttpResultMonad.State;
using MaybeMonad;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.HttpResultMonad.HttpResultWithError
{
    public class HttpResultWithErrorTests
    {
        [Fact]
        public void Ok_result_IsSuccess_is_true()
        {
            var result = HttpResultError.Ok<string>();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_result_IsFailure_is_false()
        {
            var result = HttpResultError.Ok<string>();
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsSuccess_is_false()
        {
            var result = HttpResultError.Fail("abc");
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsFailure_equals_true()
        {
            var result = HttpResultError.Fail("abc");
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_error_of_fail_result_returns_error()
        {
            var error = "abc";
            var result = HttpResultError.Fail(error);
            var isEqual = result.Error.Equals(error);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_error_of_ok_result_throws_exception()
        {
            var result = HttpResultError.Ok<string>();
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Error;
            });
        }

        [Fact]
        public void OK_HttpResult_withouth_passing_in_http_state_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResultError.Ok<string>();
            httpResult.HttpState.ShouldBe(Maybe<HttpState>.Nothing);
        }


        [Fact]
        public void Fail_HttpResult_withouth_passing_in_http_state_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResultError.Fail("abc");
            httpResult.HttpState.ShouldBe(Maybe<HttpState>.Nothing);
        }

        [Fact]
        public void From_if_predicate_is_true_returns_ok_result()
        {
            var error = "error";
            var result = HttpResultError.From(() => true, error);
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void From_if_predicate_is_false_returns_fail_result_with_error()
        {
            var error = "error";
            var result = HttpResultError.From(() => false, error);
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(error);
        }

        [Fact]
        public void Combine_if_all_results_are_ok_returns_ok_result()
        {
            var resultsLists = new List<HttpResultError<string>>
            {
                HttpResultError.Ok<string>(),
                HttpResultError.Ok<string>(),
                HttpResultError.Ok<string>()
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Combine_returns_first_fail_result_if_at_least_one_result_is_a_fail()
        {
            var firstHttpState = Test.CreateHttpStateA();
            var secondHttpState = Test.CreateHttpStateB();

            var firstFailure = HttpResultError.Fail("error", firstHttpState);
            var resultsLists = new List<HttpResultError<string>>
            {
                HttpResultError.Ok<string>(),
                firstFailure,
                HttpResultError.Ok<string>(),
                HttpResultError.Fail("second error",secondHttpState)
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
            combinedResult.Error.ShouldBe(firstFailure.Error);
            combinedResult.HttpState.ShouldBe(firstFailure.HttpState);
        }
    }
}
