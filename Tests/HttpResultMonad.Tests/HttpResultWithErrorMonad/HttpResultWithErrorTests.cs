using System;
using System.Collections.Generic;
using HttpResultMonad.State;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithErrorMonad
{
    [Trait("Monad", "HttpResultWithError")]
    public class HttpResultWithErrorTests
    {
        [Fact]
        public void Creating_fail_HttpResultWithError_with_null_as_error_throws_exception()
        {
            var exception = Should.Throw<ArgumentNullException>(() => HttpResultWithError.Fail<string>(null));
            exception.Message.ShouldStartWith(HttpResultMessages.FailureResultMustHaveError);
        }

        [Fact]
        public void Ok_HttpResultWithError_IsSuccess_is_true()
        {
            var result = HttpResultWithError.Ok<string>();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_HttpResultWithError_IsFailure_is_false()
        {
            var result = HttpResultWithError.Ok<string>();
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_HttpResultWithError_IsSuccess_is_false()
        {
            var result = HttpResultWithError.Fail("abc");
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_HttpResultWithError_IsFailure_equals_true()
        {
            var result = HttpResultWithError.Fail("abc");
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_error_of_fail_HttpResultWithError_returns_error()
        {
            var error = "abc";
            var result = HttpResultWithError.Fail(error);
            var isEqual = result.Error.Equals(error);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_error_of_ok_HttpResultWithError_throws_exception()
        {
            var result = HttpResultWithError.Ok<string>();
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Error;
            });
        }

        [Fact]
        public void OK_HttpResult_withouth_passing_in_HttpState_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResultWithError.Ok<string>();
            httpResult.HttpState.ShouldBe(HttpState.Empty);
        }


        [Fact]
        public void Fail_HttpResult_withouth_passing_in_HttpState_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResultWithError.Fail("abc");
            httpResult.HttpState.ShouldBe(HttpState.Empty);
        }
        
        [Fact]
        public void Combine_if_all_HttpResultWithError_are_ok_returns_ok_HttpResultWithError()
        {
            var resultsLists = new List<HttpResultWithError<string>>
            {
                HttpResultWithError.Ok<string>(),
                HttpResultWithError.Ok<string>(),
                HttpResultWithError.Ok<string>()
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Combine_returns_first_fail_HttpResultWithError_if_at_least_one_HttpResultWithError_is_a_fail()
        {
            var firstHttpState = Test.CreateHttpStateA();
            var secondHttpState = Test.CreateHttpStateB();

            var firstFailure = HttpResultWithError.Fail("error", firstHttpState);
            var resultsLists = new List<HttpResultWithError<string>>
            {
                HttpResultWithError.Ok<string>(HttpState.Empty),
                firstFailure,
                HttpResultWithError.Ok<string>(HttpState.Empty),
                HttpResultWithError.Fail("second error",secondHttpState)
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
            combinedResult.Error.ShouldBe(firstFailure.Error);
            combinedResult.HttpState.ShouldBe(firstFailure.HttpState);
        }
    }
}
