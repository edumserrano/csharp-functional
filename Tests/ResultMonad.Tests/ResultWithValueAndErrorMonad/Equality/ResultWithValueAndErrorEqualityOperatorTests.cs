using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "ResultWithValueAndError")]
    public class ResultWithValueAndErrorEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_ResultWithValueAndError_is_true_if_both_values_are_equal()
        {
            var value = "abc";
            var result1 = Result.Ok<string, string>(value);
            var result2 = Result.Ok<string, string>(value);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_ResultWithValueAndError_is_false_if_both_values_are_not_equals()
        {
            var result1 = Result.Ok<string, string>("abc");
            var result2 = Result.Ok<string, string>("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_ResultWithValueAndError_is_true_of_both_errors_are_equal()
        {
            var error = "abc";
            var result1 = Result.Fail<string, string>(error);
            var result2 = Result.Fail<string, string>(error);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }
    }
}
