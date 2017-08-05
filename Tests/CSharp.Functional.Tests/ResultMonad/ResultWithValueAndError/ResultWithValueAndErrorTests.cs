using System;
using System.Collections.Generic;
using ResultMonad;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.ResultMonad.ResultWithValueAndError
{
    public class ResultWithValueAndErrorTests
    {
        [Fact]
        public void Ok_result_IsSuccess_is_true()
        {
            var result = Result.Ok<string, string>("abc");
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_result_IsFailure_is_false()
        {
            var result = Result.Ok<string, string>("abc");
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsSuccess_is_false()
        {
            var result = Result.Fail<string, string>("abc");
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsFailure_equals_true()
        {
            var result = Result.Fail<string, string>("abc");
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_ok_result_returns_value()
        {
            var value = "abc";
            var result = Result.Ok<string, string>(value);
            var isEqual = result.Value.Equals(value);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_fail_result_throws_exception()
        {
            var result = Result.Fail<string, string>("abc");
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Value;
            });
        }


        [Fact]
        public void Acessing_the_error_of_fail_result_returns_error()
        {
            var error = "abc";
            var result = Result.Fail<string, string>(error);
            var isEqual = result.Error.Equals(error);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_error_of_ok_result_throws_exception()
        {
            var result = Result.Ok<string, string>("abc");
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Error;
            });
        }

        [Fact]
        public void From_if_predicate_is_true_returns_ok_result_with_value()
        {
            var value = 1;
            var error = "error";
            var result = Result.From(() => true, value, error);
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(value);
        }

        [Fact]
        public void From_if_predicate_is_false_returns_fail_result_with_errir()
        {
            var value = 1;
            var error = "error";
            var result = Result.From(() => false, value, error);
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(error);
        }

        [Fact]
        public void Combine_if_all_results_are_ok_returns_ok_result()
        {
            var resultsLists = new List<Result<int, string>>
            {
                Result.Ok<int,string>(1),
                Result.Ok<int,string>(2),
                Result.Ok<int,string>(3)
            };

            var combinedResult = Result.Combine(resultsLists.ToArray());
            combinedResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Combine_returns_first_fail_result_if_at_least_one_result_is_a_fail()
        {
            var firstFailure = Result.Fail<int, string>("error");
            var resultsLists = new List<Result<int, string>>
            {
                Result.Ok<int,string>(1),
                firstFailure,
                Result.Ok<int,string>(2),
                Result.Fail<int,string>("secod error")
            };

            var combinedResult = Result.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
            combinedResult.Error.ShouldBe(firstFailure.Error);
        }
    }
}
