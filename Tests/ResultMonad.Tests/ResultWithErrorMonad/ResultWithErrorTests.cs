﻿using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithErrorMonad
{
    [Trait("Monad", "ResultWithError")]
    public class ResultWithErrorTests
    {
        [Fact]
        public void Ok_result_IsSuccess_is_true()
        {
            var result = ResultWithError.Ok<string>();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_result_IsFailure_is_false()
        {
            var result = ResultWithError.Ok<string>();
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsSuccess_is_false()
        {
            var result = ResultWithError.Fail("abc");
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsFailure_equals_true()
        {
            var result = ResultWithError.Fail("abc");
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_error_of_fail_result_returns_error()
        {
            var error = "abc";
            var result = ResultWithError.Fail(error);
            var isEqual = result.Error.Equals(error);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Creating_fail_result_with_null_as_error_throws_exception()
        {
            var exception = Should.Throw<ArgumentException>(() => ResultWithError.Fail<string>(null));
            exception.Message.ShouldStartWith(ResultMessages.FailureResultMustHaveError);
        }

        [Fact]
        public void Acessing_the_error_of_ok_result_throws_exception()
        {
            var result = ResultWithError.Ok<string>();
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Error;
            });
            exception.Message.ShouldBe(ResultMessages.NoErrorForSuccess);
        }

        [Fact]
        public void From_if_predicate_is_true_returns_ok_result()
        {
            var error = "error";
            var result = ResultWithError.From(() => true, error);
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void From_if_predicate_is_false_returns_fail_result_with_error()
        {
            var error = "error";
            var result = ResultWithError.From(() => false, error);
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(error);
        }

        [Fact]
        public void Combine_if_all_results_are_ok_returns_ok_result()
        {
            var resultsLists = new List<ResultWithError<string>>
            {
                ResultWithError.Ok<string>(),
                ResultWithError.Ok<string>(),
                ResultWithError.Ok<string>()
            };

            var combinedResult = Result.Combine(resultsLists.ToArray());
            combinedResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Combine_returns_first_fail_result_if_at_least_one_result_is_a_fail()
        {
            var firstFailure = ResultWithError.Fail("error");
            var resultsLists = new List<ResultWithError<string>>
            {
                ResultWithError.Ok<string>(),
                firstFailure,
                ResultWithError.Ok<string>(),
                ResultWithError.Fail("secod error")
            };

            var combinedResult = Result.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
            combinedResult.Error.ShouldBe(firstFailure.Error);
        }

        [Fact]
        public void ToString_returns_ResultWithError_success_message_when_result_is_ok()
        {
            var result = ResultWithError.Ok<string>();
            result.ToString().ShouldBe(ResultMessages.GetSuccessResultWithErrorToStringMessage(typeof(string)));
        }

        [Fact]
        public void ToString_returns_ToString_of_error_when_result_is_fail()
        {
            var error = 1;
            var result = ResultWithError.Fail(error);
            result.ToString().ShouldBe(error.ToString());
        }
    }
}