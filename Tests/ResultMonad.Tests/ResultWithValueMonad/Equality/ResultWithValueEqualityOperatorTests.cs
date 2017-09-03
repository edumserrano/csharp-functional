using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueMonad.Equality
{
    [Trait("Monad", "ResultWithValue")]
    public class ResultWithValueEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_value_and_ok_ResultWithValue_is_true_if_values_are_equal()
        {
            var value = "abc";
            var result = Result.Ok(value);
            var isEqual = result == value;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_value_and_fail_ResultWithValue_is_false()
        {
            var value = "abc";
            var result = Result.Fail<string>();
            var isEqual = result == value;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_ok_ResultWithValue_is_true_if_values_are_equal()
        {
            var value = "abc";
            var result1 = Result.Ok(value);
            var result2 = Result.Ok(value);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_ResultWithValue_is_false_if_values_are_not_equal()
        {
            var result1 = Result.Ok("abc");
            var result2 = Result.Ok("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_ResultWithValue_is_true()
        {
            var result1 = Result.Fail<string>();
            var result2 = Result.Fail<string>();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_ResultWithValue_and_fail_ResultWithValue_is_false()
        {
            var okResult = Result.Ok("abc");
            var errorResult = Result.Fail<string>();
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }
    }
}
