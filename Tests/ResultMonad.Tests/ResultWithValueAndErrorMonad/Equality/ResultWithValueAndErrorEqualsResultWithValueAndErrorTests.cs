using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "ResultWithValueAndError")]
    public class ResultWithValueAndErrorEqualsResultWithValueAndErrorTests
    {
        [Fact]
        public void Equals_between_two_ok_ResultWithValueAndError_is_true_if_both_values_are_equal()
        {
            var value = "abc";
            var result1 = Result.Ok<string, string>(value);
            var result2 = Result.Ok<string, string>(value);
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_ResultWithValueAndError_is_false_if_both_values_are_not_equal()
        {
            var result1 = Result.Ok<string, string>("abc");
            var result2 = Result.Ok<string, string>("zzz");
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_two_fail_ResultWithValueAndError_is_true_if_both_errors_are_equal()
        {
            var error = "abc";
            var result1 = Result.Fail<string, string>(error);
            var result2 = Result.Fail<string, string>(error);
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_ResultWithValueAndError_is_false_if_both_errors_are_not_equal()
        {
            var result1 = Result.Fail<string, string>("abc");
            var result2 = Result.Fail<string, string>("zzz");
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_ResultWithValueAndError_and_fail_ResultWithValueAndError_is_false()
        {
            var result1 = Result.Ok<string, string>("abc");
            var result2 = Result.Fail<string, string>("abc");
            var isEqual = result1.Equals(result2);
            var isEqual2 = result2.Equals(result1);
            isEqual.ShouldBeFalse();
            isEqual2.ShouldBeFalse();
        }
    }
}
