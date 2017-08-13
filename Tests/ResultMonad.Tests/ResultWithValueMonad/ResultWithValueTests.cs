using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueMonad
{
    [Trait("Monad", "ResultWithValue")]
    public class ResultWithValueTests
    {
        [Fact]
        public void Ok_ResultWithValue_IsSuccess_is_true()
        {
            var result = Result.Ok("abc");
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_ResultWithValue_IsFailure_is_false()
        {
            var result = Result.Ok("abc");
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_ResultWithValue_IsSuccess_is_false()
        {
            var result = Result.Fail<string>();
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_ResultWithValue_IsFailure_equals_true()
        {
            var result = Result.Fail<string>();
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_ok_ResultWithValue_returns_value()
        {
            var value = "abc";
            var result = Result.Ok(value);
            var isEqual = result.Value.Equals(value);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Acessing_the_value_of_fail_ResultWithValue_throws_exception()
        {
            var result = Result.Fail<string>();
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = result.Value;
            });
            exception.Message.ShouldBe(ResultMessages.NoValueForFailure);
        }

        [Fact]
        public void Creating_ok_ResultWithValue_with_null_value_throws_exception()
        {
            var exception = Should.Throw<ArgumentException>(() => Result.Ok<string>(null));
            exception.Message.ShouldStartWith(ResultMessages.SuccessResultMustHaveValue);
        }


        [Fact]
        public void From_if_predicate_is_true_returns_ok_ResultWithValue_with_value()
        {
            var value = 1;
            var result = Result.From(() => true, value);
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(value);
        }

        [Fact]
        public void From_if_predicate_is_false_returns_fail_ResultWithValue()
        {
            var result = Result.From(() => false, "value if predicate is true");
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Combine_if_all_ResultWithValue_are_ok_returns_ok_ResultWithValue()
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
        public void Combine_returns_fail_ResultWithValue_if_at_least_one_ResultWithValue_is_a_fail()
        {
            var resultsLists = new List<Result<string>>
            {
                Result.Ok("value"),
                Result.Fail<string>(),
                Result.Ok("value"),
            };

            var combinedResult = Result.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void ToString_returns_ToString_of_value_when_ResultWithValue_is_ok()
        {
            var value = 1;
            var result = Result.Ok(value);
            result.ToString().ShouldBe(value.ToString());
        }

        [Fact]
        public void ToString_returns_ResultWithValue_error_message_when_ResultWithValue_is_fail()
        {
            var result = Result.Fail<string>();
            result.ToString().ShouldBe(ResultMessages.GetFailureResultToStringMessage(typeof(string)));
        }
    }
}
