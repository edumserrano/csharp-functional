using System;
using System.Collections.Generic;
using ResultMonad;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.ResultMonad.ResultWithValue
{
    public class ResultWithValueTests
    {
        [Fact]
        public void Ok_result_IsSuccess_is_true()
        {
            var result = Result.Ok("abc");
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_result_IsFailure_is_false()
        {
            var result = Result.Ok("abc");
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsSuccess_is_false()
        {
            var result = Result.Fail<string>();
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsFailure_equals_true()
        {
            var result = Result.Fail<string>();
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_ok_result_returns_value()
        {
            var value = "abc";
            var result = Result.Ok(value);
            var isEqual = result.Value.Equals(value);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_fail_result_throws_exception()
        {
            var result = Result.Fail<string>();
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Value;
            });
        }

        [Fact]
        public void From_if_predicate_is_true_returns_ok_result_with_value()
        {
            var value = 1;
            var result = Result.From(() => true, value);
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(value);
        }

        [Fact]
        public void From_if_predicate_is_false_returns_fail_result()
        {
            var result = Result.From(() => false, "value if predicate is true");
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Combine_if_all_results_are_ok_returns_ok_result()
        {
            var resultsLists = new List<Result<string>>
            {
                Result.Ok("value"),
                Result.Ok("value"),
                Result.Ok("value")
            };

            var combinedResult = Result.Combine(resultsLists.ToArray());
            combinedResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Combine_returns_fail_result_if_at_least_one_result_is_a_fail()
        {
            var resultsLists = new List<Result<string>>
            {
                Result.Ok<string>("value"),
                Result.Fail<string>(),
                Result.Ok<string>("value"),
            };

            var combinedResult = Result.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
        }
    }
}
